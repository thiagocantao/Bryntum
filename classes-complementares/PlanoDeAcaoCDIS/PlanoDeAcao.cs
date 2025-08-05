using CDIS.Properties;
using DevExpress.Web;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
//using CDIS;

namespace CDIS
{
    public class PlanoDeAcao : System.Web.UI.Page
    {
        public ASPxGridView gvToDoList;

        private ClasseDados dados;
        private int codigoEntidade;
        private int codigoUsuarioResponsavel;
        private int codigoProjetoAssociado;
        private int codigoTipoAssociacao;
        private Int64 codigoObjetoAssociado;
        private Unit width;
        private int VerticalScrollableHeight;
        private bool somenteLeitura;
        int[] listaUsuariosResponsaveis;
        private bool preencherPorCodigo;
        private bool mostraBotaoFullScreen = false;
        private string nomeToDoList = "";

        private int codigoToDoList;
        private ASPxHiddenField hfGeralToDoList = new ASPxHiddenField();
        private string nomeBDEmpresa;
        private string nomeOwnerEmpresa;
        private int nCallbackPageSize = 200;
        private int larguraPopup;

        public PlanoDeAcao(ClasseDados dados, int codigoEntidade, int codigoUsuarioResponsavel, int? codigoProjetoAssociado, int codigoTipoAssociacao, Int64 codigoObjetoAssociado, Unit width, int VerticalScrollableHeight, bool somenteLeitura, int[] listaUsuariosResponsaveis, bool mostraBotaoFullScreen, string nomeToDoList, int larguraPopup = 1000)
        {
            Resources.Culture = Thread.CurrentThread.CurrentUICulture;
            this.codigoToDoList = -1;
            this.dados = dados;
            this.codigoEntidade = codigoEntidade;
            this.codigoUsuarioResponsavel = codigoUsuarioResponsavel;
            this.codigoProjetoAssociado = -1;
            if (codigoProjetoAssociado.HasValue)
                this.codigoProjetoAssociado = codigoProjetoAssociado.Value;
            this.codigoTipoAssociacao = codigoTipoAssociacao;
            this.codigoObjetoAssociado = codigoObjetoAssociado;
            this.width = width;
            this.VerticalScrollableHeight = VerticalScrollableHeight;
            this.somenteLeitura = somenteLeitura;
            this.listaUsuariosResponsaveis = listaUsuariosResponsaveis;
            this.preencherPorCodigo = false;
            this.mostraBotaoFullScreen = mostraBotaoFullScreen;
            this.nomeToDoList = nomeToDoList;
            this.larguraPopup = larguraPopup;
            Session["TDL_" + codigoTipoAssociacao + "_" + codigoObjetoAssociado] = listaUsuariosResponsaveis;
        }

        public ASPxPanel constroiInterfaceFormulario(int codigoToDoList)
        {
            this.codigoToDoList = codigoToDoList;
            this.preencherPorCodigo = true;
            return constroiInterfaceFormulario();
        }

        public ASPxPanel constroiInterfaceFormulario()
        {
            // cria o objeto hidden field
            hfGeralToDoList.ID = "hfGeralToDoList";
            hfGeralToDoList.ClientInstanceName = hfGeralToDoList.ID;
            renderizaToDoList();

            ASPxPanel pnExternoToDoList = new ASPxPanel();
            pnExternoToDoList.Width = width;
            pnExternoToDoList.ID = "pnExternoToDoList";
            pnExternoToDoList.ClientInstanceName = "pnExternoToDoList";

            pnExternoToDoList.Controls.Add(gvToDoList);
            pnExternoToDoList.Controls.Add(hfGeralToDoList);


            gvToDoList.JSProperties["cp_Msg"] = "";
            if (!Page.IsCallback)
            {
                gvToDoList.DataBind();
            }

            //gvToDoList.Columns[0].Visible = true;

            return pnExternoToDoList;
        }

        #region ToDoList

        private DataSet getToDoList()
        {
            string comandoSQL = string.Format(
                @"SELECT CodigoTarefa, DescricaoTarefa, Prioridade, 
                         CodigoUsuarioResponsavelTarefa, CodigoStatusTarefa, 
                         InicioPrevisto, TerminoPrevisto, EsforcoPrevisto, CustoPrevisto, 
                         InicioReal, TerminoReal, EsforcoReal, CustoReal,
                         Anotacoes, PercentualConcluido, CodigoToDoList
                FROM TarefaToDoList");

            string sWhere;

            if (preencherPorCodigo)
                sWhere = string.Format(@" WHERE codigoToDoList = {0}", codigoToDoList);
            else
            {
                sWhere = string.Format(
                    @" WHERE codigoToDoList IN (select codigoToDoList 
                                         from ToDoList 
                                        where CodigoTipoAssociacao = {0}
                                          AND CodigoObjetoAssociado = {1}
                                          AND codigoEntidade = {2} ) 
               ", codigoTipoAssociacao, codigoObjetoAssociado, codigoEntidade);
            }

            comandoSQL = comandoSQL + sWhere + " ORDER BY codigoTarefa ";

            DataSet ds = dados.getDataSet(comandoSQL);
            if (ds != null && ds.Tables[0] != null & ds.Tables[0].Rows.Count > 0)
                codigoToDoList = int.Parse(ds.Tables[0].Rows[0]["CodigoToDoList"].ToString());

            return ds;
        }
        private string getNomeToDoList()
        {
            string nomeToDo = "";

            string comandoSQL = string.Format(
                    @"SELECT NomeToDoList 
                        FROM ToDoList 
                       WHERE CodigoTipoAssociacao = {0}
                         AND CodigoObjetoAssociado = {1}
                         AND codigoEntidade = {2} 
               ", codigoTipoAssociacao, codigoObjetoAssociado, codigoEntidade);

            DataSet ds = dados.getDataSet(comandoSQL);
            if (ds != null && ds.Tables[0] != null & ds.Tables[0].Rows.Count > 0)
                nomeToDo = ds.Tables[0].Rows[0]["NomeToDoList"].ToString();

            return nomeToDo;
        }

        // método responsável por criar a grid do ToDoList
        private void renderizaToDoList()
        {
            gvToDoList = new ASPxGridView();
            gvToDoList.ID = "gvToDoList";
            gvToDoList.Width = new Unit("100%");// new Unit((width.Value - 22) + "px");
            gvToDoList.KeyFieldName = "CodigoTarefa";
            gvToDoList.ClientInstanceName = "gvToDoList";
            gvToDoList.Settings.HorizontalScrollBarMode = ScrollBarMode.Visible;
            gvToDoList.Settings.ShowGroupPanel = true;
            gvToDoList.ClientSideEvents.Init = @"function(s, e) {
            if(window.top.lpAguardeMasterPage != undefined)
            {
                 window.top.lpAguardeMasterPage.Hide();
            }             
            var height = Math.max(0, document.documentElement.clientHeight - 110);
            s.SetHeight(height);
}";
            gvToDoList.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;

            gvToDoList.Font.Name = "Verdana";
            gvToDoList.Font.Size = new FontUnit("8pt");

            gvToDoList.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            gvToDoList.SettingsEditing.Mode = GridViewEditingMode.PopupEditForm;
            gvToDoList.SettingsPopup.EditForm.HorizontalAlign = PopupHorizontalAlign.WindowCenter;
            gvToDoList.SettingsPopup.EditForm.VerticalAlign = PopupVerticalAlign.WindowCenter;
            gvToDoList.SettingsEditing.EditFormColumnCount = 6;
            gvToDoList.SettingsPopup.EditForm.Width = new Unit(larguraPopup + "px");
            gvToDoList.SettingsPopup.EditForm.Modal = true;


            gvToDoList.SettingsEditing.NewItemRowPosition = GridViewNewItemRowPosition.Top;

            // Cria a primeira coluna - "Ações"
            gvToDoList.SettingsText.CommandNew = Resources.Incluir;
            gvToDoList.SettingsText.CommandEdit = Resources.Editar;
            gvToDoList.SettingsText.CommandDelete = Resources.Excluir;
            gvToDoList.SettingsText.CommandUpdate = Resources.Salvar;
            gvToDoList.SettingsText.CommandCancel = Resources.Cancelar;
            gvToDoList.SettingsText.EmptyDataRow = Resources.OPlanoDeAçãoAindaNãoPossuiTarefasCliqueNoB;

            gvToDoList.RowInserting += gvToDoList_RowInserting;
            gvToDoList.RowUpdating += gvToDoList_RowUpdating;
            gvToDoList.RowDeleting += gvToDoList_RowDeleting;
            gvToDoList.CustomCallback += gvToDoList_CustomCallback;
            gvToDoList.CellEditorInitialize += gvToDoList_CellEditorInitialize;
            gvToDoList.CommandButtonInitialize += gvToDoList_CommandButtonInitialize;
            gvToDoList.CustomErrorText += gvToDoList_CustomErrorText;

            GridViewCommandColumn column = new GridViewCommandColumn();
            column.ButtonRenderMode = GridCommandButtonRenderMode.Image;
            column.ShowNewButton = false;
            column.ShowEditButton = true;
            column.ShowDeleteButton = true;

            gvToDoList.SettingsCommandButton.NewButton.Image.Url = "~/imagens/botoes/incluirReg02.png";
            gvToDoList.SettingsCommandButton.EditButton.Image.Url = somenteLeitura ? "~/imagens/botoes/editarRegDes.png" : "~/imagens/botoes/editarReg02.png";
            gvToDoList.SettingsCommandButton.DeleteButton.Image.Url = somenteLeitura ? "~/imagens/botoes/excluirRegDes.png" : "~/imagens/botoes/excluirReg02.png";
            gvToDoList.SettingsCommandButton.UpdateButton.Image.Url = "~/imagens/botoes/salvar.gif";
            gvToDoList.SettingsCommandButton.CancelButton.Image.Url = "~/imagens/botoes/cancelar.gif";

            column.Caption = Resources.Ações;
            column.Width = new Unit("70px");
            column.VisibleIndex = 0;
            column.FixedStyle = GridViewColumnFixedStyle.Left;
            column.HeaderTemplate = new MyBotaoNovoTemplate(codigoProjetoAssociado, codigoTipoAssociacao, codigoObjetoAssociado, codigoToDoList, codigoEntidade, somenteLeitura, listaUsuariosResponsaveis, mostraBotaoFullScreen);
            column.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

            gvToDoList.Columns.Add(column);

            DataSet dsToDo = getToDoList();

            // if (dsToDo != null && dsToDo.Tables[0] != null & dsToDo.Tables[0].Rows.Count > 0)
            //   codigoToDoList = int.Parse(dsToDo.Tables[0].Rows[0]["CodigoToDoList"].ToString());

            DataTable dtToDo = dsToDo.Tables[0];// new DataTable();

            foreach (DataColumn dc in dsToDo.Tables[0].Columns)
            {
                string fieldName = dc.ColumnName;
                Type fieldType = dc.DataType;

                GridViewColumn newColumn = null;

                if (fieldName == "CodigoTarefa" || fieldName == "PercentualConcluido")
                {
                    newColumn = new GridViewDataTextColumn();
                    newColumn.Visible = false;
                    newColumn.Caption = fieldName == "CodigoTarefa" ? Resources.CodigoDaTarefa : Resources.PercentualConcluido;
                    ((GridViewDataTextColumn)newColumn).FieldName = fieldName;

                }
                else if (fieldName == "DescricaoTarefa")
                {
                    newColumn = new GridViewDataTextColumn();
                    ((GridViewDataTextColumn)newColumn).Caption = Resources.Tarefa;
                    ((GridViewDataTextColumn)newColumn).EditFormSettings.Caption = Resources.Tarefa_DoisPontos;
                    ((GridViewDataTextColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataTextColumn)newColumn).EditFormSettings.VisibleIndex = 2;
                    ((GridViewDataTextColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    ((GridViewDataTextColumn)newColumn).EditFormSettings.ColumnSpan = 6;
                    ((GridViewDataTextColumn)newColumn).PropertiesTextEdit.ValidationSettings.RequiredField.IsRequired = true;
                    ((GridViewDataTextColumn)newColumn).PropertiesTextEdit.MaxLength = 250;
                    ((GridViewDataTextColumn)newColumn).FieldName = fieldName;
                    ((GridViewDataTextColumn)newColumn).PropertiesTextEdit.ClientInstanceName = "DescricaoTarefa";
                    ((GridViewDataTextColumn)newColumn).HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                    newColumn.Width = 325;
                }
                else if (fieldName == "Prioridade")
                {
                    newColumn = new GridViewDataComboBoxColumn();
                    ((GridViewDataComboBoxColumn)newColumn).Caption = Resources.Prioridade;
                    ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.Caption = Resources.Prioridade_DoisPontos;
                    ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.VisibleIndex = 3;
                    ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    ((GridViewDataComboBoxColumn)newColumn).FieldName = fieldName;
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.Items.Add(Resources.PrioridadeBaixa, "B");
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.Items.Add(Resources.PrioridadeMédia, "M");
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.Items.Add(Resources.PrioridadeAlta, "A");
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = true;
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ClientInstanceName = "Prioridade";
                    ((GridViewDataComboBoxColumn)newColumn).HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;


                    newColumn.Width = 70;
                }
                else if (fieldName == "CodigoUsuarioResponsavelTarefa")
                {
                    newColumn = new GridViewDataComboBoxColumn();
                    ((GridViewDataComboBoxColumn)newColumn).Caption = Resources.Responsável;
                    ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.Caption = Resources.Responsável_DoisPontos;
                    ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.VisibleIndex = 4;
                    ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.ColumnSpan = 2;
                    ((GridViewDataComboBoxColumn)newColumn).FieldName = fieldName;
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.DataSource = getItemsUsuarios("", codigoEntidade);
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.TextField = "Descricao";
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValueField = "Codigo";
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = true;
                    newColumn.Width = 180;
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.EnableCallbackMode = true;
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.CallbackPageSize = nCallbackPageSize;
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ClientInstanceName = "CodigoUsuarioResponsavelTarefa";
                    ((GridViewDataComboBoxColumn)newColumn).HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                }
                else if (fieldName == "CodigoStatusTarefa")
                {
                    newColumn = new GridViewDataComboBoxColumn();
                    ((GridViewDataComboBoxColumn)newColumn).Caption = Resources.Status;
                    ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.Caption = Resources.Status_DoisPontos;
                    ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.VisibleIndex = 5;
                    ((GridViewDataComboBoxColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    ((GridViewDataComboBoxColumn)newColumn).FieldName = fieldName;
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ClientInstanceName = "CodigoStatusTarefa";
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.Items.AddRange(getItemsStatusTarefa());
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ValidationSettings.RequiredField.IsRequired = true;
                    ((GridViewDataComboBoxColumn)newColumn).PropertiesComboBox.ClientSideEvents.SelectedIndexChanged = @"
                    function(s, e)
                    {      
                        var dataAtual = new Date();

                        if(s.GetValue() != '2') //Em execução, cancelada e não executada.
                        {
                            DataTerminoReal.SetValue(null);
                            Anotacoes.SetEnabled(true);
                            CustoReal.SetEnabled(true);
                            EsforcoReal.SetEnabled(true);
                            DataTerminoReal.SetEnabled(true);
                            DataInicioReal.SetEnabled(true);
                            CustoPrevisto.SetEnabled(true);
                            EsforcoPrevisto.SetEnabled(true);
                            DataTerminoPrevisto.SetEnabled(true);
                            DataInicioPrevisto.SetEnabled(true);
                            CodigoUsuarioResponsavelTarefa.SetEnabled(true);
                            Prioridade.SetEnabled(true);
                            DescricaoTarefa.SetEnabled(true);
                        }
                        if(s.GetValue() == '1' || s.GetValue() == '4')// Não iniciada(4) ou Em execução(1)
                        {
                            if(DataInicioPrevisto.GetValue() == null){
                                DataInicioPrevisto.SetValue(dataAtual);
                            }
                            if(DataTerminoPrevisto.GetValue() == null){
                                DataTerminoPrevisto.SetValue(dataAtual);
                            }
                            if(s.GetValue() == '4'){
                                DataTerminoReal.SetValue(null);
                                DataInicioReal.SetValue(null);
                                EsforcoReal.SetValue(null);
                                CustoReal.SetValue(null);
                            }
                            if(s.GetValue() == '1'){
                                if(DataInicioReal.GetValue() == null){
                                    DataInicioReal.SetValue(dataAtual);
                                }
                                if(DataTerminoReal.GetValue() != null){
                                    DataTerminoReal.SetValue(null);
                                }
                            }
                        }
                        if(s.GetValue() == '2') // Concluída
                        {                             
                             if(EsforcoPrevisto.GetValue() != null && EsforcoReal.GetValue() == null){
                                EsforcoReal.SetValue(EsforcoPrevisto.GetValue());
                             }

                             if(CustoPrevisto.GetValue() != null && CustoReal.GetValue() == null){
                                CustoReal.SetValue(CustoPrevisto.GetValue());
                             }

                            if(DataInicioReal.GetValue() == null){
                                DataInicioReal.SetValue(dataAtual);
                            }
                            if(DataTerminoReal.GetValue() == null){
                                DataTerminoReal.SetValue(dataAtual);
                            }
                        }

                        DataTerminoReal.Validate();
                        DataInicioReal.Validate();
                        EsforcoReal.Validate();
                        CustoReal.Validate();
                    }";
                    newColumn.Width = 80;
                }
                else if (fieldName == "InicioPrevisto")
                {
                    newColumn = new GridViewDataDateColumn();
                    newColumn.Visible = true;
                    ((GridViewDataDateColumn)newColumn).Caption = Resources.InícioPrevisto;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.Caption = Resources.InícioPrevisto_DoisPontos;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.VisibleIndex = 6;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.ValidationSettings.RequiredField.IsRequired = true;
                    ((GridViewDataDateColumn)newColumn).FieldName = fieldName;
                    ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.ClientInstanceName = "DataInicioPrevisto";
                    ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.ClientSideEvents.Validation = @"
                    function(s, e)
                    {                       
                        DataTerminoPrevisto.Validate();                                                 
                    }";
                    newColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    newColumn.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.PopupHorizontalAlign = PopupHorizontalAlign.OutsideLeft;
                    newColumn.Width = 110;
                    ((GridViewDataDateColumn)newColumn).HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                }
                else if (fieldName == "TerminoPrevisto")
                {
                    newColumn = new GridViewDataDateColumn();
                    newColumn.Visible = true;
                    ((GridViewDataDateColumn)newColumn).Caption = Resources.TérminoPrevisto;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.Caption = Resources.TérminoPrevisto_DoisPontos;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.VisibleIndex = 7;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.ValidationSettings.RequiredField.IsRequired = true;
                    ((GridViewDataDateColumn)newColumn).FieldName = fieldName;
                    ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.ClientInstanceName = "DataTerminoPrevisto";
                    ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.ClientSideEvents.Validation = @"
                    function(s, e)
                    {
                        var dtInicioPrevisto = new Date(DataInicioPrevisto.GetValue());
                        var meuDtInicioPrevisto = (dtInicioPrevisto.getMonth() + 1) + '/' + dtInicioPrevisto.getDate() + '/' + dtInicioPrevisto.getFullYear();
                        dtInicioPrevisto = Date.parse(meuDtInicioPrevisto);

                        var dtTerminoPrevisto = new Date(DataTerminoPrevisto.GetValue());
                        var meuDtTerminoPrevisto = (dtTerminoPrevisto.getMonth() + 1) + '/' + dtTerminoPrevisto.getDate() + '/' + dtTerminoPrevisto.getFullYear();
                        dtTerminoPrevisto = Date.parse(meuDtTerminoPrevisto);

                        if (dtTerminoPrevisto < dtInicioPrevisto)
                        {
                            e.isValid = false;
                            e.errorText = '" + Resources.ADataDeTérminoPrevistaDeveSerMaiorOuIgualADataDeInício + @"';
                        }
                    }";
                    newColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    newColumn.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.PopupHorizontalAlign = PopupHorizontalAlign.OutsideLeft;
                    ((GridViewDataDateColumn)newColumn).HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                    newColumn.Width = 110;
                }
                else if (fieldName == "EsforcoPrevisto")
                {
                    newColumn = new GridViewDataSpinEditColumn();
                    newColumn.Visible = false;
                    ((GridViewDataSpinEditColumn)newColumn).Caption = Resources.EsforçoPrevistoH;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.Caption = Resources.EsforçoPrevistoH_DoisPontos;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.VisibleIndex = 8;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    ((GridViewDataSpinEditColumn)newColumn).FieldName = fieldName;
                    newColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Style.Font.Name = "Verdana";
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Style.Font.Size = 8;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Width = new Unit("100%");
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DecimalPlaces = 2;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DisplayFormatInEditMode = true;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NumberFormat = SpinEditNumberFormat.Custom;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DisplayFormatString = "N" + 2;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.SpinButtons.Visible = false;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NumberType = SpinEditNumberType.Float;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NullDisplayText = "";
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NullText = "";
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Style.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)newColumn).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.ClientInstanceName = "EsforcoPrevisto";
                    newColumn.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)newColumn).HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                }
                else if (fieldName == "CustoPrevisto")
                {
                    newColumn = new GridViewDataSpinEditColumn();
                    newColumn.Visible = false;
                    ((GridViewDataSpinEditColumn)newColumn).Caption = Resources.CustoPrevisto;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.Caption = Resources.CustoPrevisto_DoisPontos;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.VisibleIndex = 9;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    ((GridViewDataSpinEditColumn)newColumn).FieldName = fieldName;
                    newColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Style.Font.Name = "Verdana";
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Style.Font.Size = 8;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Width = new Unit("100%");
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DecimalPlaces = 2;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DisplayFormatInEditMode = true;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NumberFormat = SpinEditNumberFormat.Custom;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DisplayFormatString = "N" + 2;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.SpinButtons.Visible = false;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NumberType = SpinEditNumberType.Float;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NullDisplayText = "";
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NullText = "";
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Style.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)newColumn).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.ClientInstanceName = "CustoPrevisto";
                    ((GridViewDataSpinEditColumn)newColumn).HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                    newColumn.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                else if (fieldName == "InicioReal")
                {
                    newColumn = new GridViewDataDateColumn();
                    newColumn.Visible = true;
                    ((GridViewDataDateColumn)newColumn).Caption = Resources.InícioReal;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.Caption = Resources.InícioReal_DoisPontos;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.VisibleIndex = 10;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    ((GridViewDataDateColumn)newColumn).FieldName = fieldName;
                    ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.ClientInstanceName = "DataInicioReal";
                    ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.ClientSideEvents.Validation = @"
                    function(s, e)
                    {
                        var dataAtual = new Date();
                        var meuDataAtual = (dataAtual.getMonth() + 1) + '/' + dataAtual.getDate() + '/' + dataAtual.getFullYear();
                        dataAtual = Date.parse(meuDataAtual);

                        var dtInicioReal = new Date(DataInicioReal.GetValue());
                        var meuDataInicioReal = (dtInicioReal.getMonth() + 1) + '/' + dtInicioReal.getDate() + '/' + dtInicioReal.getFullYear();
                        dtInicioReal = Date.parse(meuDataInicioReal);

                        if (dtInicioReal > dataAtual)
                        {
                            e.isValid = false;
                            e.errorText = '" + Resources.ADataDeInícioRealDeveSerMenorOuIgualADataAtual + @"';
                        }
                        else if (DataTerminoReal.GetValue() != null && DataInicioReal.GetValue() == null)
                        {
                            e.isValid = false;
                            e.errorText = '" + Resources.ADataDeInicioRealDeveSerInformada + @"';
                        }else if(DataInicioReal.GetValue() != null && CodigoStatusTarefa.GetValue() == '4'){
                            e.isValid = false;
                            e.errorText = '" + Resources.ADataDeInicioRealNaoPodeSerInformadaParaEsteStatusDeTarefa + @"';
                        }else if(DataInicioReal.GetValue() == null && (CodigoStatusTarefa.GetValue() == '2' || CodigoStatusTarefa.GetValue() == '1')){
                            e.isValid = false;
                            e.errorText = '" + Resources.ADataDeInicioRealDeveSerInformadaParaEsteStatusDeTarefa + @"';
                        }
                    }";
                    newColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    newColumn.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    newColumn.Width = 110;
                    ((GridViewDataDateColumn)newColumn).HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                }
                else if (fieldName == "TerminoReal")
                {
                    newColumn = new GridViewDataDateColumn();
                    newColumn.Visible = true;
                    ((GridViewDataDateColumn)newColumn).Caption = Resources.TérminoReal;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.Caption = Resources.TérminoReal_DoisPontos;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.VisibleIndex = 11;
                    ((GridViewDataDateColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    ((GridViewDataDateColumn)newColumn).FieldName = fieldName;
                    ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.ClientInstanceName = "DataTerminoReal";
                    ((GridViewDataDateColumn)newColumn).PropertiesDateEdit.ClientSideEvents.Validation = @"
                    function(s, e)
                    {
                        var dataAtual = new Date();
                        var meuDataAtual = (dataAtual.getMonth() + 1) + '/' + dataAtual.getDate() + '/' + dataAtual.getFullYear();
                        dataAtual = Date.parse(meuDataAtual);

                        var dtTerminoReal = new Date(DataTerminoReal.GetValue());
                        var meuDataTerminoReal = (dtTerminoReal.getMonth() + 1) + '/' + dtTerminoReal.getDate() + '/' + dtTerminoReal.getFullYear();
                        dtTerminoReal = Date.parse(meuDataTerminoReal);

                        var dtInicioReal = new Date(DataInicioReal.GetValue());
                        var meuDataInicioReal = (dtInicioReal.getMonth() + 1) + '/' + dtInicioReal.getDate() + '/' + dtInicioReal.getFullYear();
                        dtInicioReal = Date.parse(meuDataInicioReal);

                        if(DataTerminoReal.GetValue() != null && DataInicioReal.GetValue() != null)
                        {
                              CodigoStatusTarefa.SetValue('2');  
                        }

                        if ((CodigoStatusTarefa.GetValue() == '4' || CodigoStatusTarefa.GetValue() == '1') && DataTerminoReal.GetValue() != null)
                        {
                            e.isValid = false;
                            e.errorText = '" + Resources.ADataDeTerminoRealNaoPodeSerInformadaParaEsteStatusDeTarefa + @"';
                        }
                        else if (dtTerminoReal > dataAtual)
                        {
                            e.isValid = false;
                            e.errorText = '" + Resources.ADataDeTérminoRealDeveSerMenorOuIgualADataAtual + @"';
                        }
                        else if (CodigoStatusTarefa.GetValue() == '2' && DataTerminoReal.GetValue() == null)
                        {
                            e.isValid = false;
                            e.errorText = '" + Resources.ADataDeTérminoRealDeveSerInformada + @"';
                        }
                        else if (DataTerminoReal.GetValue() != null && (dtTerminoReal < dtInicioReal))
                        {
                            e.isValid = false;
                            e.errorText = '" + Resources.ADataDeTérminoRealDeveSerMaiorOuIgualADataDeInício + @"';
                        }


                    }";
                    newColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    newColumn.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    newColumn.Width = 110;
                    ((GridViewDataDateColumn)newColumn).HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                }
                else if (fieldName == "EsforcoReal")
                {
                    newColumn = new GridViewDataSpinEditColumn();
                    newColumn.Visible = false;
                    ((GridViewDataSpinEditColumn)newColumn).Caption = Resources.EsforçoRealizadoH;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.Caption = Resources.EsforçoRealizadoH_DoisPontos;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.VisibleIndex = 12;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    ((GridViewDataSpinEditColumn)newColumn).FieldName = fieldName;
                    newColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Style.Font.Name = "Verdana";
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Style.Font.Size = 8;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Width = new Unit("100%");
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DecimalPlaces = 2;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DisplayFormatInEditMode = true;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NumberFormat = SpinEditNumberFormat.Custom;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DisplayFormatString = "N" + 2;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.SpinButtons.Visible = false;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NumberType = SpinEditNumberType.Float;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NullDisplayText = "";
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NullText = "";
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Style.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)newColumn).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.ClientInstanceName = "EsforcoReal";
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.ClientSideEvents.Validation = @"
                    function(s, e)
                    {
                        if (CodigoStatusTarefa.GetValue() == '4' && EsforcoReal.GetValue() != null)
                        {
                            e.isValid = false;
                            e.errorText = '" + Resources.OEsforcoRealNaoPodeSerInformadoParaEsteStatusDeTarefa + @"';
                        }

                    }";
                    newColumn.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)newColumn).HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                }
                else if (fieldName == "CustoReal")
                {
                    newColumn = new GridViewDataSpinEditColumn();
                    newColumn.Visible = false;
                    ((GridViewDataSpinEditColumn)newColumn).Caption = Resources.CustoRealizado;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.Caption = Resources.CustoRealizado_DoisPontos;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.VisibleIndex = 13;
                    ((GridViewDataSpinEditColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    ((GridViewDataSpinEditColumn)newColumn).FieldName = fieldName;
                    newColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Style.Font.Name = "Verdana";
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Style.Font.Size = 8;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Width = new Unit("100%");
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DecimalPlaces = 2;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DisplayFormatInEditMode = true;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NumberFormat = SpinEditNumberFormat.Custom;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.DisplayFormatString = "N" + 2;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.SpinButtons.Visible = false;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NumberType = SpinEditNumberType.Float;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NullDisplayText = "";
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.NullText = "";
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.Style.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)newColumn).CellStyle.HorizontalAlign = HorizontalAlign.Right;
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.ClientInstanceName = "CustoReal";
                    ((GridViewDataSpinEditColumn)newColumn).PropertiesSpinEdit.ClientSideEvents.Validation = @"
                    function(s, e)
                    {
                        if (CodigoStatusTarefa.GetValue() == '4' && CustoReal.GetValue() != null)
                        {
                            e.isValid = false;
                            e.errorText = '" + Resources.OCustoRealNaoPodeSerInformadaParaEsteStatusDeTarefa + @"';
                        }

                    }";
                    ((GridViewDataSpinEditColumn)newColumn).HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.True;
                    newColumn.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                }
                else if (fieldName == "Anotacoes")
                {
                    newColumn = new GridViewDataMemoColumn();
                    ((GridViewDataMemoColumn)newColumn).Visible = false;
                    ((GridViewDataMemoColumn)newColumn).Caption = Resources.Anotações;
                    ((GridViewDataMemoColumn)newColumn).EditFormSettings.Caption = Resources.Anotações_DoisPontos;
                    ((GridViewDataMemoColumn)newColumn).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
                    ((GridViewDataMemoColumn)newColumn).EditFormSettings.VisibleIndex = 14;
                    ((GridViewDataMemoColumn)newColumn).EditFormSettings.ColumnSpan = 6;
                    ((GridViewDataMemoColumn)newColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
                    ((GridViewDataMemoColumn)newColumn).PropertiesMemoEdit.Rows = 3;
                    ((GridViewDataMemoColumn)newColumn).FieldName = fieldName;
                    ((GridViewDataMemoColumn)newColumn).PropertiesMemoEdit.ClientInstanceName = "Anotacoes";
                    ((GridViewDataMemoColumn)newColumn).PropertiesMemoEdit.Style.CssClass = "Resize";
                }

                gvToDoList.Font.Size = new FontUnit("8pt");
                gvToDoList.Font.Name = "Verdana";
                gvToDoList.StylesEditors.Style.Font.Size = new FontUnit("8pt");
                gvToDoList.StylesEditors.Style.Font.Name = "Verdana";

                if (newColumn != null)
                    gvToDoList.Columns.Add(newColumn);
            }

            GridViewDataTextColumn colunaPlanoAcao = new GridViewDataTextColumn();
            ((GridViewDataTextColumn)colunaPlanoAcao).Caption = " ";
            ((GridViewDataTextColumn)colunaPlanoAcao).EditFormSettings.Caption = Resources.PlanoDeAção_DoisPontos;
            ((GridViewDataTextColumn)colunaPlanoAcao).EditFormSettings.CaptionLocation = ASPxColumnCaptionLocation.Top;
            ((GridViewDataTextColumn)colunaPlanoAcao).EditFormSettings.VisibleIndex = 0;
            ((GridViewDataTextColumn)colunaPlanoAcao).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
            ((GridViewDataTextColumn)colunaPlanoAcao).EditFormSettings.ColumnSpan = 6;
            ((GridViewDataTextColumn)colunaPlanoAcao).ReadOnly = true;
            ((GridViewDataTextColumn)colunaPlanoAcao).PropertiesTextEdit.Style.BackColor = Color.FromName("#EBEBEB");
            ((GridViewDataTextColumn)colunaPlanoAcao).PropertiesTextEdit.Style.ForeColor = Color.Black;
            ((GridViewDataTextColumn)colunaPlanoAcao).PropertiesTextEdit.ClientSideEvents.Init = "function(s,e){s.SetText(hfGeralToDoList.Get('NomeToDoList'));}";
            ((GridViewDataTextColumn)colunaPlanoAcao).Visible = false;

            gvToDoList.Columns.Add(colunaPlanoAcao);
            gvToDoList.EditFormLayoutProperties.ColCount = 6;

            gvToDoList.DataSource = dtToDo;
        }

        private string AjustaValorCampoData(DateTime data)
        {
            string dia = data.Day.ToString();
            string mes = data.Month.ToString();
            string ano = data.Year.ToString();
            string horax = data.TimeOfDay.ToString();
            string hora = data.Hour.ToString();
            string minuto = data.Minute.ToString();
            string segundo = data.Second.ToString();
            return string.Format("{0:n2}/{1:n2}/{2} {3:n2}:{4:n2}:{5:n2}", dia, mes, ano, hora, minuto, segundo);
        }

        private int atualizaToDoList(char tipoOperacao, int codigoTarefa, OrderedDictionary valoresCampos)
        {
            int codigoTarefaRetorno = -1;
            string mensagem = "";

            // Prepara as informações a serem salvas na lista de tarefas
            DateTime DataInicioPrevisto = new DateTime();
            DateTime DataTerminoPrevisto = new DateTime();
            DateTime DataInicioReal = new DateTime();
            DateTime DataTerminoReal = new DateTime();

            // DescricaoTarefa
            string DescricaoTarefa = valoresCampos["DescricaoTarefa"] != null ? valoresCampos["DescricaoTarefa"].ToString().Replace("'", "'+char(39)+'") : "";
            DescricaoTarefa = "'" + DescricaoTarefa + "'";

            // InicioPrevisto
            string InicioPrevisto = "null";
            if (valoresCampos["InicioPrevisto"] != null && valoresCampos["InicioPrevisto"].ToString() != "")
            {
                DataInicioPrevisto = DateTime.Parse(valoresCampos["InicioPrevisto"].ToString());
                InicioPrevisto = AjustaValorCampoData(DataInicioPrevisto);
                InicioPrevisto = " convert(datetime, '" + InicioPrevisto + "', 103)";
            }            
            //TerminoPrevisto
            string TerminoPrevisto = "null";
            if (valoresCampos["TerminoPrevisto"] != null && valoresCampos["TerminoPrevisto"].ToString() != "")
            {
                DataTerminoPrevisto = DateTime.Parse(valoresCampos["TerminoPrevisto"].ToString());
                TerminoPrevisto = AjustaValorCampoData(DataTerminoPrevisto);
                TerminoPrevisto = " convert(datetime, '" + TerminoPrevisto + "', 103)";
            }
            //EsforcoPrevisto
            string EsforcoPrevisto = valoresCampos["EsforcoPrevisto"] != null && valoresCampos["EsforcoPrevisto"].ToString() != "" ? valoresCampos["EsforcoPrevisto"].ToString() : "null";
            EsforcoPrevisto = EsforcoPrevisto.Replace(".", "");
            EsforcoPrevisto = EsforcoPrevisto.Replace(",", ".");


            //CustoPrevisto
            string CustoPrevisto = valoresCampos["CustoPrevisto"] != null && valoresCampos["CustoPrevisto"].ToString() != "" ? valoresCampos["CustoPrevisto"].ToString() : "null";
            CustoPrevisto = CustoPrevisto.Replace(".", "");
            CustoPrevisto = CustoPrevisto.Replace(",", ".");

            // InicioReal
            string InicioReal = "null";
            if (valoresCampos["InicioReal"] != null && valoresCampos["InicioReal"].ToString() != "")
            {
                DataInicioReal = DateTime.Parse(valoresCampos["InicioReal"].ToString());
                InicioReal = AjustaValorCampoData(DataInicioReal);
                InicioReal = " convert(datetime, '" + InicioReal + "', 103)";
            }
            //TerminoReal
            string TerminoReal = "null";
            if (valoresCampos["TerminoReal"] != null && valoresCampos["TerminoReal"].ToString() != "")
            {
                DataTerminoReal = DateTime.Parse(valoresCampos["TerminoReal"].ToString());
                TerminoReal = AjustaValorCampoData(DataTerminoReal);
                TerminoReal = " convert(datetime, '" + TerminoReal + "', 103)";
            }
            //EsforcoReal
            string EsforcoReal = valoresCampos["EsforcoReal"] != null && valoresCampos["EsforcoReal"].ToString() != "" ? valoresCampos["EsforcoReal"].ToString() : "null";
            EsforcoReal = EsforcoReal.Replace(".", "");
            EsforcoReal = EsforcoReal.Replace(",", ".");

            //CustoReal
            string CustoReal = valoresCampos["CustoReal"] != null && valoresCampos["CustoReal"].ToString() != "" ? valoresCampos["CustoReal"].ToString() : "null";
            CustoReal = CustoReal.Replace(".", "");
            CustoReal = CustoReal.Replace(",", ".");

            //CodigoUsuarioResponsavelTarefa
            string CodigoUsuarioResponsavelTarefa = valoresCampos["CodigoUsuarioResponsavelTarefa"] != null && valoresCampos["CodigoUsuarioResponsavelTarefa"].ToString() != "" ? valoresCampos["CodigoUsuarioResponsavelTarefa"].ToString() : "null";

            //Prioridade
            string Prioridade = valoresCampos["Prioridade"] != null && valoresCampos["Prioridade"].ToString() != "" ? "'" + valoresCampos["Prioridade"].ToString() + "'" : "'B'";

            //CodigoStatusTarefa
            string CodigoStatusTarefa = valoresCampos["CodigoStatusTarefa"] != null && valoresCampos["CodigoStatusTarefa"].ToString() != "" ? valoresCampos["CodigoStatusTarefa"].ToString() : "null";

            //Anotacoes
            string Anotacoes = valoresCampos["Anotacoes"] != null && valoresCampos["Anotacoes"].ToString() != "" ? "'" + valoresCampos["Anotacoes"].ToString().Replace("'", "'+char(39)+'") + "'" : "null";

            // Algumas consistências.
            // A data prevista de termino deve ser maior que a de início
            if (DataTerminoPrevisto < DataInicioPrevisto)
            {
                mensagem = Resources.ADataDeTérminoPrevistaDeveSerMaiorOuIgualADataDeInicio;
            }
            // a data de Inicio deve ser menor/igual a data atual
            else if (DataInicioReal > DateTime.Now)
            {
                mensagem = Resources.ADataDeInícioRealDeveSerMenorOuIgualADataAtual;
            } 
            // a data de inicio esta preenchida e o status da tarefa está como Não Iniciada
            else if (InicioReal != "null" && CodigoStatusTarefa == "4")
            {
                mensagem = Resources.ADataDeInicioRealNaoPodeSerInformadaParaEsteStatusDeTarefa;
            }
            // a data de inicio não esta preenchida e o status da tarefa está como Concluída ou Em execução
            else if (InicioReal == "null" && (CodigoStatusTarefa == "2" || CodigoStatusTarefa == "1"))
            {
                mensagem = Resources.ADataDeInicioRealDeveSerInformadaParaEsteStatusDeTarefa;
            }
            // a data de termino deve ser menor/igual a data atual
            else if (DataTerminoReal > DateTime.Now)
            {
                mensagem = Resources.ADataDeTérminoRealDeveSerMenorOuIgualADataAtual;
            }
            // se tem data de termino real e o status da tarefa está como Em execução ou não iniciada 
            else if (TerminoReal != "null" && (CodigoStatusTarefa == "4" || CodigoStatusTarefa == "1"))
            {
                mensagem = Resources.ADataDeTerminoRealNaoPodeSerInformadaParaEsteStatusDeTarefa;
            }
            // se tem data de termino real, tem que ter a data de início real
            else if (TerminoReal != "null" && InicioReal == "null")
            {
                mensagem = Resources.ADataDeInicioRealDeveSerInformada;
            }
            // se tem data de termino real, deve ser maior que a de início real 
            else if (TerminoReal != "null" && DataTerminoReal < DataInicioReal)
            {
                mensagem = Resources.ADataDeTérminoRealDeveSerMaiorOuIgualADataDeInicio;
            }
            else if (CodigoStatusTarefa == "2" && TerminoReal == "null")
            {
                mensagem = Resources.ADataDeTérminoRealDeveSerInformada;
            }

            else if (CodigoStatusTarefa == "4" && EsforcoReal != "null")
            {
                mensagem = Resources.OEsforcoRealNaoPodeSerInformadoParaEsteStatusDeTarefa;
            }

            else if (CodigoStatusTarefa == "4" && CustoReal != "null")
            {
                mensagem = Resources.OCustoRealNaoPodeSerInformadaParaEsteStatusDeTarefa;
            }

            //grava(mensagem);

            if (mensagem == "")
            {

                string comandoSQL = "";
                if (tipoOperacao == 'E') // Edição
                {
                    comandoSQL = string.Format(
                        @" UPDATE TarefaToDoList 
                      SET DescricaoTarefa = {1}
                        , InicioPrevisto =  {2}
                        , TerminoPrevisto = {3}
                        , EsforcoPrevisto = {4}
                        , CustoPrevisto = {5}
                        , InicioReal = {6}
                        , TerminoReal = {7}
                        , EsforcoReal = {8}
                        , CustoReal = {9}
                        , Prioridade = {10}
                        , CodigoStatusTarefa = {11}
                        , Anotacoes = {12}
                        , CodigoUsuarioResponsavelTarefa = {13}
                        , CodigoUsuarioUltimaAlteracao = {14}
                        , DataUltimaAlteracao = getdate()
                    WHERE CodigoTarefa = {0}", codigoTarefa,
                           DescricaoTarefa,
                           InicioPrevisto, TerminoPrevisto, EsforcoPrevisto, CustoPrevisto,
                           InicioReal, TerminoReal, EsforcoReal, CustoReal,
                           Prioridade, CodigoStatusTarefa, Anotacoes, CodigoUsuarioResponsavelTarefa, codigoUsuarioResponsavel);

                    int afetatos = 0;
                    dados.execSQL(comandoSQL, ref afetatos);
                }
                else // Inclusao 
                {
                    codigoObjetoAssociado = Int64.Parse(hfGeralToDoList.Get("codigoObjetoAssociado").ToString());

                    string nomeToDoListInsert = "";

                    if (hfGeralToDoList.Contains("NomeToDoList"))
                    {
                        nomeToDoListInsert = hfGeralToDoList.Get("NomeToDoList").ToString().Replace("'", "'+char(39)+'");

                        if (nomeToDoListInsert.Length > 250)
                            nomeToDoListInsert = nomeToDoListInsert.Substring(0, 250);
                    }

                    comandoSQL = string.Format(
                        @"BEGIN
                        DECLARE @CodigoToDoList int    
                        DECLARE @CodigoTarefa bigint
                            SET @CodigoToDoList = {0}

                        if (@CodigoToDoList<=0)
                        BEGIN
                            INSERT INTO ToDoList (DataInclusao, CodigoUsuarioInclusao, CodigoTipoAssociacao, CodigoObjetoAssociado, CodigoEntidade, CodigoUsuarioResponsavelToDoList, NomeToDoList)
                                VALUES (getdate(), {1}, {2}, {3}, {4}, {1}, '{5}' ) 
                            SET @CodigoToDoList = SCOPE_IDENTITY()
                        END
                     ", codigoToDoList, codigoUsuarioResponsavel, codigoTipoAssociacao, codigoObjetoAssociado, codigoEntidade, nomeToDoListInsert);

                    comandoSQL += string.Format(
                        @" INSERT INTO TarefaToDoList 
                        (CodigoToDoList, DescricaoTarefa, 
                         InicioPrevisto, TerminoPrevisto, EsforcoPrevisto, CustoPrevisto, 
                         InicioReal, TerminoReal, EsforcoReal, CustoReal,
                         CodigoUsuarioResponsavelTarefa, Prioridade, CodigoStatusTarefa, Anotacoes, codigoProjeto, 
                         DataInclusao, CodigoUsuarioInclusao)
                   VALUES (@CodigoToDoList, {1},
                           {2}, {3}, {4}, {5},
                           {6}, {7}, {8}, {9},
                           {10}, {11}, {12}, {13}, {14}, 
                           GETDATE(), {15})

                   SELECT @CodigoTarefa = scope_identity()
                    
                   SELECT @CodigoToDoList as CodigoToDoList, 
                          @CodigoTarefa as CodigoTarefa
                    
               END", "",
                           DescricaoTarefa,
                           InicioPrevisto, TerminoPrevisto, EsforcoPrevisto, CustoPrevisto,
                           InicioReal, TerminoReal, EsforcoReal, CustoReal,
                           CodigoUsuarioResponsavelTarefa, Prioridade, CodigoStatusTarefa, Anotacoes,
                           codigoProjetoAssociado > 0 ? codigoProjetoAssociado.ToString() : "null", codigoUsuarioResponsavel);

                    DataTable dtRet = dados.getDataSet(comandoSQL).Tables[0];
                    codigoToDoList = int.Parse(dtRet.Rows[0]["CodigoToDoList"].ToString());
                    codigoTarefaRetorno = int.Parse(dtRet.Rows[0]["CodigoTarefa"].ToString());
                }
            }

            gvToDoList.JSProperties["cp_Msg"] = mensagem;

            return codigoTarefaRetorno;
        }

        protected void gvToDoList_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            int codigoTarefa = atualizaToDoList('I', -1, e.NewValues);
            Session["_CodigoToDoList_"] = codigoToDoList;
            hfGeralToDoList.Set("_CodigoToDoList_", codigoToDoList);

            (sender as ASPxGridView).DataSource = getToDoList().Tables[0];
            (sender as ASPxGridView).DataBind();

            e.Cancel = true;
            (sender as ASPxGridView).CancelEdit();
        }

        protected void gvToDoList_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int codigoTarefa = int.Parse(e.Keys["CodigoTarefa"].ToString());
            int codigoToDoListaRetorno = atualizaToDoList('E', codigoTarefa, e.NewValues);


            (sender as ASPxGridView).DataSource = getToDoList().Tables[0];
            (sender as ASPxGridView).DataBind();

            e.Cancel = true;
            (sender as ASPxGridView).CancelEdit();

        }

        protected void gvToDoList_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string codigoStatus = e.Values["CodigoStatusTarefa"].ToString();

            if (codigoStatus != "1")
            {
                gvToDoList.JSProperties["cp_Msg"] = Resources.SomenteTarefasEmExecuçãoPodemSerExcluídas_Exclamacao;
                e.Cancel = true;
            }
            else
            {
                int codigoTarefa = int.Parse(e.Keys["CodigoTarefa"].ToString());
                string comandoSQL = string.Format(
                    @" DELETE TarefaToDoList 
                    WHERE CodigoTarefa = {0}", codigoTarefa);

                int afetatos = 0;
                dados.execSQL(comandoSQL, ref afetatos);
                (sender as ASPxGridView).DataSource = getToDoList().Tables[0];
                (sender as ASPxGridView).DataBind();

                e.Cancel = true;
            }
        }

        protected void gvToDoList_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            //gvToDoList.Columns[0].Visible = !somenteLeitura;

            if (e.Parameters == "Popular")
            {
                codigoObjetoAssociado = Int64.Parse(hfGeralToDoList.Get("codigoObjetoAssociado").ToString());
                gvToDoList.DataSource = getToDoList().Tables[0];
                gvToDoList.DataBind();
            }
        }

        protected void gvToDoList_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName != "CodigoStatusTarefa" && e.VisibleIndex != -1 && !gvToDoList.IsNewRowEditing)
            {
                string codigoStatus = gvToDoList.GetRowValues(e.VisibleIndex, "CodigoStatusTarefa").ToString();

                if (codigoStatus != "1")
                {
                    e.Editor.ClientEnabled = false;
                    e.Editor.DisabledStyle.BackColor = Color.FromName("#EBEBEB");
                    e.Editor.DisabledStyle.ForeColor = Color.Black;
                }
            }
            if (e.Column.FieldName == "Prioridade")
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                ListEditItem li = null;
                object prioridade = gvToDoList.GetRowValues(e.VisibleIndex, "Prioridade");

                if (prioridade == null)
                {
                    li = cmb.Items.FindByValue("B");
                    li.Selected = true;
                }
            }
        }

        protected void gvToDoList_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {
            e.ErrorText = e.Exception.Message;
        }

        protected void gvToDoList_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {

            if (e.VisibleIndex != -1)
            {
                if (e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    string codigoStatus = gvToDoList.GetRowValues(e.VisibleIndex, "CodigoStatusTarefa").ToString();

                    if (codigoStatus != "1" || somenteLeitura)
                    {
                        e.Image.Url = "~/imagens/botoes/excluirRegDes.png";
                        e.Enabled = false;
                    }
                }
                else if (e.ButtonType == ColumnCommandButtonType.Edit)
                {
                    if (somenteLeitura)
                    {
                        e.Image.Url = "~/imagens/botoes/editarRegDes.png";
                        e.Enabled = false;
                    }
                }
            }
        }

        protected void btnNovoToDo_Click(object sender, EventArgs e)
        {
            gvToDoList.AddNewRow();
        }

        #endregion

        private void ajustaNomeBDEmpresa()
        {
            //busca o nome do banco de dados da empresa 
            string comandoSQL = string.Format(
                 @"SELECT NomeBDProjeto, NomeOwnerProjeto 
                FROM {0}.{1}.Empresa 
               WHERE CodigoEmpresa = {2}", dados.databaseNameCdis, dados.OwnerdbCdis, codigoEntidade);
            DataSet ds = dados.getDataSet(comandoSQL);
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                nomeBDEmpresa = ds.Tables[0].Rows[0]["NomeBDProjeto"].ToString();
                nomeOwnerEmpresa = ds.Tables[0].Rows[0]["NomeOwnerProjeto"].ToString();
            }
            else
                gvToDoList.JSProperties["cp_Msg"] = Resources.AsInformaçõesDaEmpresaInformadaNãoForamEnc;
        }

        private string ajustaComandoSQLCamposLookup_E_CampoPreDefinido(string comandoSQLOriginal)
        {
            string comandoSQL = string.Format(
                @"SELECT De, Para
                    FROM {0}.{1}.DeParaObjetosDB
                   ORDER By De", dados.databaseNameCdis, dados.OwnerdbCdis);
            DataSet ds = dados.getDataSet(comandoSQL);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                int De = int.Parse(dr["De"].ToString());
                string Para = dr["Para"].ToString().ToUpper();
                // se for campo reservado - #CodigoProjeto#, #Entidade#, #Usuario#
                if (Para == "#CODIGOPROJETO#")
                {
                    comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", codigoProjetoAssociado.ToString());
                }
                else if (Para == "#ENTIDADE#")
                {
                    comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", codigoEntidade.ToString());
                }
                else if (Para == "#USUARIO#")
                {
                    comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", codigoUsuarioResponsavel.ToString());
                }

                else
                    comandoSQLOriginal = comandoSQLOriginal.Replace("{" + De + "}", Para);
            }

            return comandoSQLOriginal;
        }

        private DataTable getUsuariosLista(int[] listaCodigosUsuarios, int codigoEntidade)
        {
            string where = "";
            foreach (int codigoUsuario in listaCodigosUsuarios)
            {
                where += codigoUsuario.ToString() + ", ";
            }

            if (where != "")
            {
                where = where.Substring(0, where.Length - 2);
                where = "AND us.CodigoUsuario in (" + where + ")";
                string comandoSQL = string.Format(
                  @"SELECT us.CodigoUsuario as Codigo
                         , CASE WHEN uun.IndicaUsuarioAtivoUnidadeNegocio = 'S' THEN us.NomeUsuario ELSE us.NomeUsuario + ' (INATIVO)' END as Descricao
                  FROM {0}.{1}.Usuario us
                    INNER JOIN UsuarioUnidadeNegocio uun on us.CodigoUsuario = uun.CodigoUsuario
                  WHERE uun.CodigoUnidadeNegocio IN (SELECT un.CodigoEntidade FROM UnidadeNegocio un WHERE un.CodigoUnidadeNegocio = {2})
                   {3}
                 ORDER BY IndicaUsuarioAtivoUnidadeNegocio DESC, us.NomeUsuario", dados.databaseNameCdis, dados.OwnerdbCdis, codigoEntidade, where);
                DataSet ds = dados.getDataSet(comandoSQL);
                if (ds != null && ds.Tables[0] != null)
                {
                    return ds.Tables[0];
                }
            }
            return null;
        }

        private DataTable getTableOpcoesListaPre(int codigoListaPre, string iniciaisLookup)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("codigo");
            dt.Columns.Add("descricao");

            if (nomeBDEmpresa == null || nomeBDEmpresa == "")
                ajustaNomeBDEmpresa();

            // monta o WHERE de pesquisa na tabela Lookup. Se o código for menor ou igual a zero, utilizaremos as iniciais
            string where = " WHERE CodigoLookup = " + codigoListaPre;
            if (codigoListaPre <= 0 && iniciaisLookup.Trim() != "")
                where = "WHERE IniciaisLookup = '" + iniciaisLookup + "' ";

            // busca o comando sql para recuperar o valor do campo pré-definido
            string comandoSQL = string.Format(
                @"SELECT ComandoRetornoLookup
                    FROM {0}.{1}.Lookup
                     {2}", dados.databaseNameCdis, dados.OwnerdbCdis, where);
            DataSet ds = dados.getDataSet(comandoSQL);
            if (ds != null && ds.Tables[0] != null)
            {
                string ComandoRetornoCampo = ds.Tables[0].Rows[0]["ComandoRetornoLookup"].ToString();
                ComandoRetornoCampo = ajustaComandoSQLCamposLookup_E_CampoPreDefinido(ComandoRetornoCampo);
                // executa o comando pré-definido
                try
                {
                    DataSet ds2 = dados.getDataSet(ComandoRetornoCampo);
                    if (ds2 != null && ds2.Tables[0] != null)
                    {
                        foreach (DataRow dr in ds2.Tables[0].Rows)
                        {
                            dt.Rows.Add(dr["Codigo"], dr["Descricao"]);
                        }
                    }
                }
                catch { }
            }
            return dt;
        }

        private DataTable getItemsUsuarios(string where, int codigoEntidade)
        {
            ListEditItemCollection items = new ListEditItemCollection();
            DataTable dtUsuarios;
            if (listaUsuariosResponsaveis != null)
            {
                dtUsuarios = getUsuariosLista(listaUsuariosResponsaveis, codigoEntidade);
            }
            else
            {
                dtUsuarios = getTableOpcoesListaPre(-1, "INTP");
            }
            //foreach (DataRow dr in dtUsuarios.Rows)
            //{
            //    items.Add(dr["Descricao"].ToString(), int.Parse(dr["Codigo"].ToString()));
            //}

            return dtUsuarios;
        }

        private ListEditItemCollection getItemsStatusTarefa()
        {
            ListEditItemCollection items = new ListEditItemCollection();

            // Monta o comando sql para recuperar a lista de usuários
            string comandoSQL = string.Format(
                @"SELECT CodigoStatusTarefa as Codigo, DescricaoStatusTarefa as Descricao
                    FROM {0}.{1}.StatusTarefa order by DescricaoStatusTarefa", dados.databaseNameCdis, dados.OwnerdbCdis);
            DataSet ds = dados.getDataSet(comandoSQL);
            if (ds != null && ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    items.Add(dr["Descricao"].ToString(), int.Parse(dr["Codigo"].ToString()));
                }
            }
            return items;
        }

        public void grava(String texto)
        {
            using (StreamWriter outputFile = new StreamWriter("c:\\temp\\log.dat", true))
            {
                String data = DateTime.Now.ToShortDateString();
                String hora = DateTime.Now.ToShortTimeString();
                String computador = Dns.GetHostName();
                outputFile.WriteLine(data + " " + hora + " (" + computador + ")" + texto);
            }
        }

    }

    class MyBotaoNovoTemplate : ITemplate
    {
        private int codigoUnidade;
        private int codigoProjetoAssociado;
        private int codigoTipoAssociacao;
        private Int64 codigoObjetoAssociado;
        private int codigoToDoList;
        private bool somenteLeitura;
        private bool mostraBotaoFullScreen = false;
        int[] listaUsuariosResponsaveis;

        public MyBotaoNovoTemplate(int? codigoProjetoAssociado, int codigoTipoAssociacao, Int64 codigoObjetoAssociado, int codigoToDoList, int codigoUnidade, bool somenteLeitura, int[] listaUsuariosResponsaveis, bool mostraBotaoFullScreen)
        {
            this.codigoProjetoAssociado = -1;
            if (codigoProjetoAssociado.HasValue)
                this.codigoProjetoAssociado = codigoProjetoAssociado.Value;
            this.codigoTipoAssociacao = codigoTipoAssociacao;
            this.codigoObjetoAssociado = codigoObjetoAssociado;
            this.codigoToDoList = codigoToDoList;
            this.codigoUnidade = codigoUnidade;
            this.somenteLeitura = somenteLeitura;
            this.listaUsuariosResponsaveis = listaUsuariosResponsaveis;
            this.mostraBotaoFullScreen = mostraBotaoFullScreen;
        }

        public void InstantiateIn(Control container)
        {
            GridViewHeaderTemplateContainer gridContainer = (GridViewHeaderTemplateContainer)container;
            ASPxImage btnNovo = new ASPxImage();
            btnNovo.ID = "btnNovo";
            btnNovo.ImageUrl = somenteLeitura ? "~/imagens/botoes/incluirRegDes.png" : "~/imagens/botoes/incluirReg02.png";
            btnNovo.ToolTip = Resources.Incluir;
            btnNovo.Border.BorderStyle = BorderStyle.None;
            btnNovo.ClientEnabled = !somenteLeitura;
            btnNovo.ClientSideEvents.Click =
                @"function(s, e) 
                    {
                        gvToDoList.AddNewRow();
                    }";

            ASPxImage btnMaximizar = new ASPxImage();
            btnMaximizar.ID = "btnMaximizar";
            btnMaximizar.ImageUrl = "~/imagens/botoes/btnMaximizarTela.png";
            btnMaximizar.ToolTip = Resources.VisualizarEmModoAmpliado;
            btnMaximizar.Border.BorderStyle = BorderStyle.None;

            string parametrosURL = string.Format(@"?CP={0}&CTA={1}&COA={2}&RO={3}&CTDL={4}&UN={5}", codigoProjetoAssociado, codigoTipoAssociacao, codigoObjetoAssociado, somenteLeitura ? "S" : "N", codigoToDoList, codigoUnidade);


            btnMaximizar.ClientSideEvents.Click =
                @"function(s, e) 
                    {
                        var url = window.top.pcModal.cp_Path + 'espacoTrabalho/tarefasPlanoAcao.aspx" + parametrosURL + @"'
                        window.top.showModal2(url + '&AT=' + (screen.height - 380), 'Plano de Trabalho - ZOOM', screen.width - 40, screen.height - 240, function(e){" + gridContainer.Grid.ClientInstanceName + @".PerformCallback()});
                    }";

            Literal lt1 = new Literal();
            lt1.Text = string.Format(@"
            <table><tr><td style='padding-right:5px'>");
            container.Controls.Add(lt1);
            container.Controls.Add(btnNovo);

            Literal lt2 = new Literal();
            lt2.Text = string.Format(@"
            </td><td>");
            container.Controls.Add(lt2);

            if (mostraBotaoFullScreen && codigoToDoList != -1)
                container.Controls.Add(btnMaximizar);

            Literal lt3 = new Literal();
            lt3.Text = string.Format(@"
            </td></tr></table>");
            container.Controls.Add(lt3);



        }
    }

}
