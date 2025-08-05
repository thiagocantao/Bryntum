var timedOutMsg;
var tipoEdicao;

var MailDemo = {

    PendingCallbacks: {},

    DoCallback: function (sender, callback) {
        if (sender.InCallback()) {
            MailDemo.PendingCallbacks[sender.name] = callback;
            sender.EndCallback.RemoveHandler(MailDemo.DoEndCallback);
            sender.EndCallback.AddHandler(MailDemo.DoEndCallback);
        } else {
            callback();
        }
    },

    DoEndCallback: function (s, e) {
        var pendingCallback = MailDemo.PendingCallbacks[s.name];
        if (pendingCallback) {
            pendingCallback();
            delete MailDemo.PendingCallbacks[s.name];
        }
    },

    // Mail page

    ClientMailTree_NodeClick: function (s, e) {
        var parametro = (e.node.name == 'S' || e.node.name == 'E') ? e.node.name : e.node.name.split(';')[0];

        hfGeral.Set("CodigoPastaSelecionada", parametro);
        ClientSearchBox.SetText("");
        MailDemo.DoCallback(ClientMailPanel, function () {
            ClientMailPanel.PerformCallback()
        });

    },

    ClientMailSplitter_PaneResized: function (s, e) {
        if (e.pane.name === "GridPane")
            ClientMailGrid.SetHeight(e.pane.GetClientHeight() - ClientMailMenu.GetMainElement().offsetHeight);
    },

    ClientMailMenu_ItemClick: function (s, e) {
        var name = e.item.name;
        tipoEdicao = name;

        hfDestinatarios.Set("Tipo", name);

        if (name === "compose" || name === "reply" || name === "replyAll" || name === "fwd") {
            gvUsuarios.SetVisible(name === "compose" || name === "fwd");
            txtDestinatarios.SetVisible(name != "compose" && name != "fwd");
            lblDestinatarios.SetVisible(name != "compose" && name != "fwd");

            if (window.ClientMailEditor)
                ClientMailEditor.SetHtml("");
            //ClientMailSendButton.SetEnabled(false);
            ClientMailEditorPopup.Show();

            var key = -1;
            if (name != "compose") {
                key = ClientMailGrid.GetRowKey(ClientMailGrid.GetFocusedRowIndex());
                MailDemo.DoCallback(ClientMailEditorPopup, function () {
                    ClientMailEditorPopup.PerformCallback(key);
                });
            }

        }
    },

    ClientMailGrid_Init: function (s, e) {
        var currentRow = s.GetFocusedRowIndex(), canReply = !!s.GetVisibleRowsOnPage() && !s.IsGroupRow(currentRow);

        ClientMailMenu.GetItemByName("reply").SetEnabled(canReply);
        ClientMailMenu.GetItemByName("replyAll").SetEnabled(canReply);
        ClientMailMenu.GetItemByName("fwd").SetEnabled(canReply);
        ClientMailMenu.GetItemByName("Print").SetEnabled(canReply);
        s.Focus();
        AdjustSize();
        document.getElementById('divGrid').style.visibility = '';
    },

    ClientMailGrid_FocusedRowChanged: function (s, e) {
        clearTimeout(timedOutMsg);
        var currentRow = s.GetFocusedRowIndex(),
            canReply = !!s.GetVisibleRowsOnPage() && !s.IsGroupRow(currentRow);

        ClientMailMenu.GetItemByName("reply").SetEnabled(canReply);
        ClientMailMenu.GetItemByName("replyAll").SetEnabled(canReply);
        ClientMailMenu.GetItemByName("fwd").SetEnabled(canReply);
        ClientMailMenu.GetItemByName("Print").SetEnabled(canReply);

        MailDemo.DoCallback(ClientMailMessagePanel, function () {
            ClientMailMessagePanel.PerformCallback(s.GetRowKey(currentRow));
            ClientMailSplitter.GetPaneByName("MessagePane").SetScrollTop(0);

        });
    },

    ClientMailGrid_EndCallback: function (s, e) {

        hfGeral.Set('CodigoMensagemMover', '-1');
        ClientMailSplitter.GetPaneByName("GridPane").RaiseResizedEvent();

    },

    ClientSearchBox_TextChanged: function (s, e) {
        MailDemo.DoCallback(ClientMailPanel, function () {
            ClientMailPanel.PerformCallback();
        });
    },

    ClientSearchBox_KeyPress: function (s, e) {
        e = e.htmlEvent;
        if (e.keyCode === 13) {
            // prevent default browser form submission
            if (e.preventDefault)
                e.preventDefault();
            else
                e.returnValue = false;
        }
    },

    ClientMailEditor_Init: function (s, e) {
        window.setTimeout(function () { s.Focus(); }, 0);
        ClientMailSendButton.SetEnabled(true);
    },

    ClientMailSendButton_Click: function (s, e) {

        if (tipoEdicao == "reply" || tipoEdicao == "replyAll") {
            if (txtDestinatarios.GetText() == "") {
                window.top.mostraMensagem(traducao.Demo_nenhum_destinat_rio_foi_selecionado_, 'erro', true, false, null);
                return;
            }
        } else if (tipoEdicao == "compose" || tipoEdicao == "fwd") {

            if (gvUsuarios.GetSelectedRowCount() == 0) {
                window.top.mostraMensagem(traducao.Demo_nenhum_destinat_rio_foi_selecionado_, 'erro', true, false, null);
                return;
            }
        }

        ClientMailPanel.PerformCallback(tipoEdicao);
        ClientMailEditorPopup.Hide();
    },

    ClientMailCancelButton_Click: function (s, e) {
        ClientMailEditorPopup.Hide();
    },

    // Calendar page

    ClientResourceCheckBox_CheckedChanged: function (s, e) {
        MailDemo.DoCallback(ClientScheduler, function () {
            ClientScheduler.Refresh();
        });
    },

    ClientScheduler_AppointmentDoubleClick: function (s, e) {
        s.ShowAppointmentFormByClientId(e.appointmentId);
        e.handled = true;
    },

    // Contacts page

    ClientContactSplitter_PaneResized: function (s, e) {
        if (e.pane.name == "GridPane")
            ClientContactGrid.SetHeight(e.pane.GetClientHeight());
    },

    ClientContactGrid_Init: function (s, e) {
        s.Focus();
    },

    ClientContactGrid_FocusedRowChanged: function (s, e) {
        MailDemo.DoCallback(ClientContactDetailsPanel, function () {
            ClientContactDetailsPanel.PerformCallback();
        });
    },

    ClientContactGrid_EndCallback: function (s, e) {
        ClientContactSplitter.GetPaneByName("GridPane").RaiseResizedEvent();
    },

    ClientContactViewOptions_SelectedIndexChanged: function (s, e) {
        MailDemo.DoCallback(ClientContactPanel, function () {
            ClientContactPanel.PerformCallback(s.GetSelectedIndex());
        });

    },

    // Feeds page

    ClientFeedNavBar_ItemClick: function (s, e) {
        MailDemo.DoCallback(ClientFeedPanel, function () {
            ClientFeedPanel.PerformCallback();
        });
    },

    ClientFeedSplitter_PaneResized: function (s, e) {
        if (e.pane.name === "GridPane")
            ClientFeedGrid.SetHeight(e.pane.GetClientHeight());
    },

    ClientFeedGrid_Init: function (s, e) {
        s.Focus();
    },

    ClientFeedGrid_FocusedRowChanged: function (s, e) {
        if (window.ClientFeedDescriptionPanel)
            MailDemo.DoCallback(ClientFeedDescriptionPanel, function () {
                ClientFeedDescriptionPanel.PerformCallback();
            });
    },

    ClientFeedGrid_EndCallback: function (s, e) {
        ClientFeedSplitter.GetPaneByName("GridPane").RaiseResizedEvent();
    }

};

function treeView_OnInit(s, e) {
    ProcessNode(s.GetRootNode());
}

function ProcessNode(node) {
    var htmlElement = node.GetHtmlElement();
    var count = node.GetNodeCount();

    if (node.name != "nome") {
        if (htmlElement != null) {
            var handler = function (evt) {
                var podeExcluir = count > 0 ? 'N' : node.name.split(';')[1];
                popupMenu_ToggleItemsVisibility(podeExcluir);
                popupMenu.cpClickedNode = node;
                popupMenu.ShowAtElement(node.GetHtmlElement());

                ASPxClientUtils.PreventEventAndBubble(evt);
            }
            ASPxClientUtils.AttachEventToElement(htmlElement, "contextmenu", handler);
        }

    }
    for (var i = 0; i < count; i++)
        ProcessNode(node.GetNode(i));
}

function popupMenu_ToggleItemsVisibility(podeExcluir) {
    popupMenu.GetItemByName("excluir").SetVisible(podeExcluir == 'S');
}

function popupMenu_OnPopUp(s, e) {
    if (s.cpClickedNode.name == 'E') {
        s.GetItemByName("opcao").SetImageUrl('../imagens/botoes/incluirReg02.png');
        s.GetItemByName("opcao").SetText("Incluir Pasta");
        pcNovaPasta.SetHeaderText(traducao.Demo_inclus_o_de_pasta___caixa_de_entrada);
    }
    else if (s.cpClickedNode.name == 'S') {
        s.GetItemByName("opcao").SetImageUrl('../imagens/botoes/incluirReg02.png');
        s.GetItemByName("opcao").SetText("Incluir Pasta");
        pcNovaPasta.SetHeaderText(traducao.Demo_inclus_o_de_pasta___caixa_de_sa_da);
    }
    else {
        s.GetItemByName("opcao").SetImageUrl('../imagens/botoes/editarReg02.png');
        s.GetItemByName("opcao").SetText("Editar Pasta");
        pcNovaPasta.SetHeaderText(traducao.Demo_edi__o_de_pasta);
    }    
}

function popupMenu_OnItemClick(s, e) {
    if (e.item.name == "opcao") {
        var parametro = (s.cpClickedNode.name == 'S' || s.cpClickedNode.name == 'E') ? s.cpClickedNode.name : s.cpClickedNode.name.split(';')[0];
        hfGeral.Set("IndicaEntradaSaida", parametro);
        txtNomePasta.SetText((s.cpClickedNode.name == 'E' || s.cpClickedNode.name == 'S') ? "" : s.cpClickedNode.text);
        pcNovaPasta.Show(); 
    }
    else {
        if(confirm(traducao.Demo_deseja_excluir_a_pasta_selecionada_))
            pnMenu.PerformCallback('X' + s.cpClickedNode.name.split(';')[0]);
    }

}

function popupMenuMover_OnItemClick(s, e) {

    if (e.item.parent != null && (e.item.parent.name == 'mover' || e.item.parent.name == 'S' || e.item.parent.name == 'E')) {
        if (confirm(traducao.Demo_deseja_mover_o_item_selecionado_para_a_pasta_ + '"' + e.item.GetText() + '"?')) {
            hfGeral.Set('PastaDestino', e.item.name);
            ClientMailPanel.PerformCallback('MV');
        }

    }
}

function lerMsg() {
    if (ClientMailGrid.GetFocusedRowIndex() != -1 && ClientMailGrid.GetRowKey(ClientMailGrid.GetFocusedRowIndex()) != null) {
        var param = 'LT' + ClientMailGrid.GetRowKey(ClientMailGrid.GetFocusedRowIndex())

        ClientMailGrid.PerformCallback(param);
    }
}

function adicionaEmail(valor) {
    var email = valor[0];

    txtUsuarios.SetText(txtUsuarios.GetText() + email + '; ');
}

function removeEmail(valor) {
    var email = valor[0];

    txtUsuarios.SetText(txtUsuarios.GetText().replace(email + '; ', ''));
}

function AdjustSize() {
    var height = Math.max(0, document.documentElement.clientHeight) - 60;
    ClientMailGrid.SetHeight(height);
}

function OnControlsInitialized(s, e) {
    ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
        AdjustSize();
    });
}
