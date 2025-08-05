<%@ Page Language="C#" AutoEventWireup="true" CodeFile="graficoEAP.aspx.cs" Inherits="GoJs_EAP_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../estilos/graficoEAP.css" rel="stylesheet" />
    <script src="../../Bootstrap/vendor/jquery/v3.3.1/jquery-3.3.1.min.js"></script>
    <script src="../../Bootstrap/vendor/jquery/mask/jquery.mask.min.js"></script>
    <link href="../../Bootstrap/vendor/bootstrap/v4.1.3/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <script src="../../bootstrap/v4.1.3/js/bootstrap.bundle.min.js?V=2018_11_8_18_18_46"></script>
    <script type="text/javascript" language="javascript" src="../js/go.js?v=1"></script>
    <script type="text/javascript" language="javascript" id="code">
        go.licenseKey = "2bf845eab36558c511895a2540383bbe5aa16f23c88c4ea20d0745f5bd5d6a1d22cde079508682c8d7ad5cec1c2a9389dd923a28c04d063fe734da8c44e08ff0b66475e2150e4488a65775c79dfe79f3ff2c77f3d0a571f78a7e8ca0bba9d18c5fe9f3d257ce11b82e";
        function trataInit() {
            if (hfGeral.Get("IDEdicaoEAP") !== '') {
                init();
            }
            else {
                var ocultadiv = document.getElementById('toolBar');
                ocultadiv.setAttribute('style', 'display:none');

            }
        }
        function init() {
            starttime();
            var $ = go.GraphObject.make;  // for conciseness in defining templates
            myDiagram =
                $(go.Diagram, "myDiagramDiv", // must be the ID or reference to div
                    {
                        initialContentAlignment: go.Spot.TopCenter,
                        // make sure users can only create trees
                        validCycle: go.Diagram.CycleDestinationTree,
                        // users can select only one part at a time
                        maxSelectionCount: 1,
                        initialScale: parseFloat(colorEditFonte.cp_EscalaZoom),
                        layout: $(go.TreeLayout, {
                            sorting: go.TreeLayout.SortingAscending,
                            comparer: function (va, vb) {
                                var da = va.node.data;
                                var db = vb.node.data;
                                if (da.EDT < db.EDT) return -1;
                                if (da.EDT > db.EDT) return 1;
                                return 0;
                            }
                        }),
                        "allowMove": false,
                        // support editing the properties of the selected person in HTML
                        "ChangedSelection": onSelectionChanged,
                        "TextEdited": onTextEdited,
                        "SelectionDeleting": onSelectionDeleting,
                        "TreeExpanded": onTreeExpanded,
                        "SelectionDeleted": onSelectionDeleted,
                        "allowClipboard": false,
                        // enable undo & redo
                        "undoManager.isEnabled": true,
                        "ClipboardPasted": onClipboardPasted,
                        "ObjectSingleClicked": onObjectSingleClicked,
                        "isModified": false

                    });

            // when the document is modified, add a "*" to the title and enable the "Save" button
            myDiagram.addDiagramListener("Modified", function (e) {
                var button = document.getElementById("SaveButton");
                if (button) button.disabled = false;
                var idx = document.title.indexOf("*");
                if (myDiagram.isModified) {
                    if (idx < 0) document.title += "*";
                } else {
                    if (idx >= 0) document.title = document.title.substr(0, idx);
                }
            });

            var levelColors = ["#2672EC/#2E8DEF"];
            var bluegrad = colorEdit.GetText();

            // override TreeLayout.commitNodes to also modify the background brush based on the tree depth level
            myDiagram.layout.commitNodes = function () {
                go.TreeLayout.prototype.commitNodes.call(myDiagram.layout);  // do the standard behavior
                // then go through all of the vertexes and set their corresponding node's Shape.fill
                //// to a brush dependent on the TreeVertex.level value      
            }

            // when a node is double-clicked, add a child to it
            function nodeDoubleClick(e, obj) {
                var clicked = obj.part;
                if (clicked !== null) {
                    var thisemp = clicked.data;
                    var nomeTarefa = escape(thisemp.name);
                    var responsavel = thisemp.CodigoUsuarioResponsavel;
                    var modo = hfGeral.Get("Acesso") && obj.part.data.nivel > 0 ? "E" : "V";
                    var possuiFilhos = "N";
                    if (responsavel == undefined)
                        responsavel = null;
                    if (obj.findTreeChildrenNodes().count > 0)
                        possuiFilhos = "S";
                    var alturaPopup = Math.max(0, document.documentElement.clientHeight) - 300;
                    var larguraPopup = Math.max(0, document.documentElement.clientWidth) - 75;
                    window.top.showModal3(window.top.pcModal.cp_Path + 'GoJs/EAP/popupEdicaoTarefaEAP.aspx?MO=' + modo + '&projectID=' + hfGeral.Get("IDEdicaoEAP") + '&GUID=' + thisemp.idTarefa + '&elementName=' + nomeTarefa + '&PossuiFilhos=' + possuiFilhos + '&CR=' + responsavel, 'Item da EAP', null, null, null)
                }
            }

            // this is used to determine feedback during drags
            function mayWorkFor(node1, node2) {
                if (!(node1 instanceof go.Node)) return false;  // must be a Node
                if (node1 === node2) return false;  // cannot work for yourself
                if (node2.isInTreeOf(node1)) return false;  // cannot work for someone who works for you
                return true;
            }

            // This function provides a common style for most of the TextBlocks.
            // Some of these values may be overridden in a particular TextBlock.
            function textStyle() {
                return { font: "9pt Verdana", stroke: "white", margin: new go.Margin(3, 3, 3, 3) };
            }

            function textStyle1() {
                return { font: "9pt Verdana", stroke: "white", margin: new go.Margin(3, 6, 6, 3), alignment: go.Spot.Center };
            }
            var noSelecionado = null;
            var noClicado = null;

            var customDate = document.getElementById("customDateEditor");
            customDate.onActivate = function () {
                customDate.style.visibility = "";
                var startingValue = customDate.textEditingTool.textBlock.text;
                // Finish immediately when a radio button is pressed
                var onClick = function (e) {
                    var tool = customDate.textEditingTool;
                    if (tool === null) return;
                    tool.acceptText(go.TextEditingTool.Tab);
                }

                customDate.value = startingValue;

                // Do a few different things when a user presses a key
                customDate.addEventListener("keydown", function (e) {
                    var keynum = e.which;
                    var tool = customDate.textEditingTool;
                    if (tool === null) return;
                    if (keynum == 13) { // Accept on Enter
                        tool.acceptText(go.TextEditingTool.Tab);
                        return;
                    } else if (keynum == 9) { // Accept on Tab
                        tool.acceptText(go.TextEditingTool.Tab);
                        e.preventDefault();
                        return false;
                    } else if (keynum === 27) { // Cancel on Esc
                        tool.doCancel();
                        if (tool.diagram) tool.diagram.focus();
                    }
                }, false);

                var loc = customDate.textEditingTool.textBlock.getDocumentPoint(go.Spot.TopLeft);
                var pos = myDiagram.transformDocToView(loc);
                customDate.style.left = (pos.x) + "px";
                customDate.style.top = (pos.y - 5) + "px";
            }

            //customDate.ondeactivate = function () { if (customDate.value != '') customDate.value = formatNumber(customDate.value.replace(',', '.')); };

            var customText = document.getElementById("customTextEditor");
            customText.onActivate = function () {
                customText.style.visibility = "";
                var startingValue = customText.textEditingTool.textBlock.text;
                // Finish immediately when a radio button is pressed
                var onClick = function (e) {
                    var tool = customText.textEditingTool;
                    if (tool === null) return;
                    tool.acceptText(go.TextEditingTool.Tab);
                }

                customText.value = startingValue;

                // Do a few different things when a user presses a key
                customText.addEventListener("keydown", function (e) {
                    var keynum = e.which;
                    var tool = customText.textEditingTool;
                    if (tool === null) return;
                    if (keynum == 13) { // Accept on Enter
                        tool.acceptText(go.TextEditingTool.Tab);
                        return;
                    } else if (keynum == 9) { // Accept on Tab
                        tool.acceptText(go.TextEditingTool.Tab);
                        e.preventDefault();
                        return false;
                    } else if (keynum === 27) { // Cancel on Esc
                        tool.doCancel();
                        if (tool.diagram) tool.diagram.focus();
                    }
                }, false);

                var loc = customText.textEditingTool.textBlock.getDocumentPoint(go.Spot.TopLeft);
                var pos = myDiagram.transformDocToView(loc);
                customText.style.left = (pos.x) + "px";
                customText.style.top = (pos.y - 5) + "px";
            }

            customText.ondeactivate = function () { if (customText.value != '') customText.value = formatNumber(customText.value.replace(',', '.')); };

            // define the Node template
            myDiagram.nodeTemplate =
                $(go.Node, "Auto",
                    {
                        doubleClick: nodeDoubleClick,
                        selectionAdorned: false,
                        resizable: true
                    },
                    { // handle dragging a Node onto a Node to (maybe) change the reporting relationship
                        mouseDragEnter: function (e, node, prev) {
                            var diagram = node.diagram;
                            var selnode = diagram.selection.first();
                            if (!mayWorkFor(selnode, node)) return;
                            var shape = node.findObject("SHAPE");
                            if (shape) {
                                shape._prevFill = shape.fill;  // remember the original brush
                                shape.fill = "darkred";
                            }
                        },
                        mouseDragLeave: function (e, node, next) {
                            var shape = node.findObject("SHAPE");
                            if (shape && shape._prevFill) {
                                shape.fill = shape._prevFill;  // restore the original brush
                            }
                        },
                        mouseDrop: function (e, node) {
                            var diagram = node.diagram;
                            var selnode = diagram.selection.first();  // assume just one Node in selection
                            var previousParent = selnode.findTreeParentNode();
                            if (mayWorkFor(selnode, node)) {
                                // find any existing link into the selected node
                                var link = selnode.findTreeParentLink();
                                if (link !== null) {  // reconnect any existing link
                                    link.fromNode = node;
                                } else {  // else create a new link
                                    diagram.toolManager.linkingTool.insertLink(node, node.port, selnode, selnode.port);
                                }
                                calculaPesosEAP(node);
                                defineEDT(selnode, true, null, previousParent);
                                window.parent.existeConteudoCampoAlterado = true;
                            }
                        }
                    },
                    // for sorting, have the Node.text be the data.name
                    new go.Binding("text", "name"),
                    // bind the Part.layerName to control the Node's layer depending on whether it isSelected
                    new go.Binding("layerName", "isSelected", function (sel) { return sel ? "Foreground" : ""; }).ofObject(),
                    // define the node's outer shape
                    $(go.Shape, "RoundedRectangle",
                        {
                            name: "SHAPE", stroke: null,
                            // set the port properties:
                            portId: "", fromLinkable: false, toLinkable: false, cursor: "pointer"
                        }),
                    $(go.Panel, "Vertical",
                        $(go.Panel, "Table",
                            {
                                minSize: new go.Size(100, 30),
                                margin: new go.Margin(5, 6, 6, 5),
                                defaultAlignment: go.Spot.Left
                            },
                            $(go.RowColumnDefinition, { column: 2, width: 4 }),
                            $(go.TextBlock, textStyle1(),
                                {
                                    row: 0, column: 0, columnSpan: 5, name: "lblEdt", textAlign: "center", editable: false, isMultiline: false, visible: true
                                },
                                new go.Binding("text", "EDT"), { name: "textBlockEDT", textAlign: "center" }),
                            $(go.TextBlock, textStyle1(),  // the name
                                {
                                    row: 1, column: 0, columnSpan: 5,
                                    editable: hfGeral.Get("Acesso"),
                                    name: "txtNome",
                                    textValidation: validaNome
                                },
                                new go.Binding("text", "name").makeTwoWay(), {
                                name: "textBlockPanel",
                                textAlign: "center",
                                wrap: go.TextBlock.None,
                                overflow: go.TextBlock.OverflowEllipsis,
                                minSize: new go.Size(140, 10),
                                maxSize: new go.Size(250, 250)
                            }),
                            $(go.TextBlock, traducao.graficoEAP_in_cio__, textStyle(),
                                {
                                    row: 2, column: 0, name: "lblInicio", editable: false, isMultiline: false, visible: false
                                }),
                            $(go.TextBlock, textStyle(),  // the name
                                {
                                    row: 2, column: 1,
                                    editable: false, isMultiline: false,
                                    visible: false, textEditor: customDate,
                                    textValidation: validaDatas,
                                    textEdited: calculaDatas
                                },
                                new go.Binding("text", "Inicio").makeTwoWay(), { name: "textBlockInicio" }),
                            $(go.TextBlock, traducao.graficoEAP_t_rmino__, textStyle(),
                                {
                                    row: 3, column: 0, name: "lblTermino", editable: false, isMultiline: false, visible: false
                                }),
                            $(go.TextBlock, textStyle(),  // the name
                                {
                                    row: 3, column: 1,
                                    editable: false, isMultiline: false,
                                    name: "txtTermino",
                                    visible: false, textEditor: customDate,
                                    textValidation: validaDatas,
                                    textEdited: calculaDatas
                                },
                                new go.Binding("text", "Termino").makeTwoWay(), { name: "textBlockTermino" }),
                            $(go.TextBlock, traducao.graficoEAP_custo__, textStyle(),
                                {
                                    row: 4, column: 0, name: "lblCusto", editable: false, isMultiline: false, visible: false
                                }),
                            $(go.TextBlock, textStyle(),  // the name
                                {
                                    row: 4, column: 1,
                                    editable: false, isMultiline: false,
                                    name: "txtCusto",
                                    visible: false, textEditor: customText,
                                    textValidation: validaNumero,
                                    textEdited: calculaDatas
                                },
                                new go.Binding("text", "Custo").makeTwoWay(), { name: "textBlockCusto" }),
                            $(go.TextBlock, traducao.graficoEAP_receita__, textStyle(),
                                {
                                    row: 5, column: 0, name: "lblReceita", editable: false, isMultiline: false, visible: false
                                }),
                            $(go.TextBlock, textStyle(),  // the name
                                {
                                    row: 5, column: 1,
                                    editable: false, isMultiline: false,
                                    name: "txtReceita",
                                    visible: false, textEditor: customText,
                                    textValidation: validaNumero,
                                    textEdited: calculaDatas
                                },
                                new go.Binding("text", "Receita").makeTwoWay(), { name: "textBlockReceita" }),
                            $(go.TextBlock, traducao.graficoEAP_trabalho__, textStyle(),
                                {
                                    row: 6, column: 0, name: "lblTrabalho", editable: false, isMultiline: false, visible: false
                                }),
                            $(go.TextBlock, textStyle(),  // the name
                                {
                                    row: 6, column: 1,
                                    editable: false, isMultiline: false,
                                    name: "txtTrabalho",
                                    visible: false, textEditor: customText,
                                    textValidation: validaNumero,
                                    textEdited: calculaDatas
                                },
                                new go.Binding("text", "Trabalho").makeTwoWay(), { name: "textBlockTrabalho" }),
                            $(go.TextBlock, traducao.graficoEAP_peso__, textStyle(),
                                {
                                    row: 7, column: 0, name: "lblPeso", editable: false, isMultiline: false, visible: false
                                }),
                            $(go.TextBlock, textStyle(),  // the name
                                {
                                    row: 7, column: 1,
                                    editable: hfGeral.Get("Acesso"), isMultiline: false,
                                    name: "txtValorPeso",
                                    visible: false, textEditor: customText,
                                    textValidation: validaNumero,
                                    textEdited: calculaPesos
                                },
                                new go.Binding("text", "ValorPeso").makeTwoWay(), { name: "textBlockValorPeso" }),
                            $(go.TextBlock, traducao.graficoEAP___peso__, textStyle(),
                                {
                                    row: 8, column: 0, name: "lblPercentualPeso", editable: false, isMultiline: false, visible: false
                                }),
                            $(go.TextBlock, textStyle(),  // the name
                                {
                                    row: 8, column: 1,
                                    editable: false, isMultiline: false,
                                    name: "txtPercentualPesoTarefa",
                                    visible: false
                                },
                                new go.Binding("text", "PercentualPesoTarefa").makeTwoWay(), { name: "textBlockPercentualPesoTarefa" }),
                            $(go.TextBlock, textStyle(),  // the name
                                {
                                    row: 9, column: 0,
                                    editable: false, isMultiline: false,
                                    name: "txtPodeEditar",
                                    visible: false
                                },
                                new go.Binding("text", "PodeEditar").makeTwoWay(), { name: "textBlockPodeEditar" }),
                            $(go.TextBlock, textStyle(),  // the name
                                {
                                    row: 10, column: 0,
                                    editable: false, isMultiline: false,
                                    name: "txtPodeExcluir",
                                    visible: false
                                },
                                new go.Binding("text", "PodeExcluir").makeTwoWay(), { name: "textBlockPodeExcluir" }),
                            $(go.TextBlock, textStyle(),  // the name
                                {
                                    row: 11, column: 0,
                                    editable: false, isMultiline: false,
                                    name: "txtPodeAdicionarFilho",
                                    visible: false
                                },
                                new go.Binding("text", "PodeAdicionarFilho").makeTwoWay(), { name: "textBlockPodeAdicionarFilho" })
                        ),// end Table Panel
                        $(go.Panel,  // this is underneath the "BODY"
                            { height: 15 },  // always this height, even if the TreeExpanderButton is not visible
                            $("TreeExpanderButton")
                        )
                    )
                );  // end Node

            // define the Link template
            myDiagram.linkTemplate =
                $(go.Link, go.Link.Orthogonal,
                    { corner: 5, relinkableFrom: true, relinkableTo: true },
                    $(go.Shape, { strokeWidth: 3, stroke: "#808080" }));  // the link shape

            // read in the JSON-format data from the "mySavedModel" element
            load();
            var rootNode = myDiagram.findTreeRoots().first();
            defineEDT(rootNode, false, '0');
            ajustaLayout();
        }

        function onObjectSingleClicked(e, s, v) {
            if (e.subject != null && !e.subject.isTreeLink)
                noClicado = e.subject;
        }

        // Allow the user to edit text when a single node is selected
        function onSelectionChanged(e) {
            var node = e.diagram.selection.first();
            noSelecionado = node;
            if (node instanceof go.Node) {
                var dados = node.data;

                node.textEditable = dados.PodeEditar == 'S';

                if (dados.PodeExcluir == 'S') {
                    node.deletable = true;
                    document.getElementById('tdExcluir').style.display = '';
                }
                else {
                    node.deletable = false;
                    document.getElementById('tdExcluir').style.display = '';
                }

                if (dados.PodeAdicionarFilho == 'S') {
                    node.copyable = true;
                    document.getElementById('tdAddFilho').style.display = '';
                    document.getElementById('tdAddIrmao').style.display = '';
                }
                else {
                    node.copyable = false;
                    document.getElementById('tdAddFilho').style.display = 'none';
                    document.getElementById('tdAddIrmao').style.display = 'none';
                }
            }
            //Encostou no EAP Salva
            salvar();
        }

        function onSelectionDeleting(e) {
            /*if (hfGeral.Get("Acesso")) {
                var obj = myDiagram.selection.first();
                e.cancel = obj.isTreeLink || obj.data.PodeExcluir === 'N';
                if (!e.cancel) {
                    indicaExclusao = true;
                    calculaDatasArvoreEAP(obj);
                    calculaPesosGeralEAP(obj, true);
                    debugger;
                    var parent = obj.findTreeParentNode();
                    defineEDT(parent, true, parent.data.EDT, null);
                    indicaExclusao = false;
                    if (obj != null && !obj.isTreeLink) {
                        excluiFilhos(obj);
                    }
                }
            }
            else {
                e.cancel = hfGeral.Get("Acesso");
            }*/
            excluir();
            e.cancel = true;
        }

        function onSelectionDeleted(e) {
            window.parent.existeConteudoCampoAlterado = true;
            calculaDatas();
            salvar();
        }

        function onClipboardPasted(e, obj, teste) {
            e.cancel = true;
            //copiaItem();
            //calculaPesosEAP(myDiagram.findTreeRoots().first());
        }

        function onTreeExpanded(e, obj) {
            //debugger
        }

        // Update the HTML elements for editing the properties of the currently selected node, if any
        function updateProperties(data) {

        }

        // This is called when the user has finished inline text-editing
        function onTextEdited(e) {
            window.parent.existeConteudoCampoAlterado = true;
            var tb = e.subject;
            if (tb === null || !tb.name) return;

            if (tb.text === "") {
                tb.text = e.parameter;
                e.cancel = true;
                return;
            }
            tb.text = tb.text.substring(0, 255);
            var node = tb.part;
            if (node instanceof go.Node) {
                updateProperties(node.data);
            }
        }
        // Update the data fields when the text is changed
        function updateData(text, field) {
            var node = myDiagram.selection.first();
            // maxSelectionCount = 1, so there can only be one Part in this collection
            var data = node.data;
            if (node instanceof go.Node && data !== null) {
                var model = myDiagram.model;
                model.startTransaction("modified " + field);
                if (field === "name") {
                    model.setDataProperty(data, "name", text);
                }
                model.commitTransaction("modified " + field);
            }
        }
        // Show the diagram's model in JSON format
        function save() {
            lpAguardeMasterPage.Show();
            verificaFechados();
            mudaCor();
            var strJson = myDiagram.model.toJson();
            document.getElementById("mySavedModel").value = strJson;
            window.parent.existeConteudoCampoAlterado = false;
            cancelaFechamentoPopUp2 = 'N';
            cancelaFechamentoPopUp = 'N';
            hfGeral.Set("Fonte", fonte);
            hfGeral.Set("Escala", myDiagram.scale);
            callbackSalvar.PerformCallback(strJson, function () {
                saveBT();
            });
        }
        function load() {
            if (fonte == null) {
                fonte = parseInt(colorEditFonte.cp_Fonte);
            }
            myDiagram.model = go.Model.fromJson(document.getElementById("mySavedModel").value);
            mudaCor();
        }
        function undo() {
            myDiagram.undoManager.undo();
            mudaCor();
            calculaPesosEAP(myDiagram.findTreeRoots().first());
        }
        function redo() {
            myDiagram.undoManager.redo();
            mudaCor();
            calculaPesosEAP(myDiagram.findTreeRoots().first());
            salvar();
        }
        function copiaItem() {
            var obj = myDiagram.selection.first();
            if (obj != null && !obj.isTreeLink) {
                var clicked = obj.part;
                if (clicked !== null) {
                    var thisemp = clicked.data;
                    myDiagram.startTransaction("Copiar Item");
                    var nextkey = (myDiagram.model.nodeDataArray.length + 1).toString();
                    var newEdt = obj.data.EDT == '0' ?
                        (obj.findTreeChildrenNodes().count + 1).toString() :
                        obj.data.EDT + '.' + (obj.findTreeChildrenNodes().count + 1);
                    var newemp = {
                        key: nextkey, name: thisemp.name, title: thisemp.title, parent: noClicado.part.data.key, idTarefa: "", nivel: (parseInt(noClicado.part.data.nivel) + 1), indicaFechado: false,
                        Inicio: thisemp.Inicio, Termino: thisemp.Termino, ValorPeso: thisemp.ValorPeso, PercentualPesoTarefa: thisemp.PercentualPesoTarefa, Custo: thisemp.Custo, Receita: thisemp.Receita, Trabalho: thisemp.Trabalho,
                        PodeEditar: "S", PodeExcluir: "S", PodeAdicionarFilho: "S", CodigoUsuarioResponsavel: thisemp.CodigoUsuarioResponsavel,
                        Anotacoes: thisemp.Anotacoes, Duracao: thisemp.Duracao, IndicaAtribuicaoManualPesoTarefa: "N", PodeEditarPeso: "S", CodigoInternoTarefa: "-1", CriterioAceitacao: thisemp.CriterioAceitacao, IndicaResumo: "N",
                        SequenciaTarefaCronograma: "", EDT: newEdt
                    };

                    myDiagram.model.addNodeData(newemp);
                    myDiagram.commitTransaction("Copiar Item");
                    mudaCor();
                    window.parent.existeConteudoCampoAlterado = true;
                    //myDiagram.findNodeForKey(nextkey).part.isSelected = true
                    //go.TextEditingTool.prototype.doActivate();
                }
            }
        }
        function addFilho() {
            var obj = myDiagram.selection.first();
            if (obj != null && !obj.isTreeLink) {
                var clicked = obj.part;
                if (clicked !== null) {
                    var thisemp = clicked.data;
                    myDiagram.startTransaction("Adicionar Item");
                    var nextkey = (myDiagram.model.nodeDataArray.length).toString();
                    var newEdt = obj.data.EDT == '0' ?
                        (obj.findTreeChildrenNodes().count + 1).toString() :
                        obj.data.EDT + '.' + (obj.findTreeChildrenNodes().count + 1);
                    var newemp = {
                        key: nextkey, name: traducao.graficoEAP_novo_item_, title: "", parent: thisemp.key, idTarefa: "", nivel: (parseInt(thisemp.nivel) + 1), indicaFechado: false,
                        Inicio: "", Termino: "", ValorPeso: "0", PercentualPesoTarefa: "0", Custo: "0", Receita: "", Trabalho: "0",
                        PodeEditar: "S", PodeExcluir: "S", PodeAdicionarFilho: "S", CodigoUsuarioResponsavel: "",
                        Anotacoes: "", Duracao: "", IndicaAtribuicaoManualPesoTarefa: "N", PodeEditarPeso: "S", CodigoInternoTarefa: "-1", CriterioAceitacao: "",
                        IndicaResumo: "N", SequenciaTarefaCronograma: "", EDT: newEdt
                    };
                    myDiagram.model.addNodeData(newemp);
                    myDiagram.commitTransaction("Adicionar Item");
                    mudaCor();
                    window.parent.existeConteudoCampoAlterado = true;
                    //myDiagram.findNodeForKey(nextkey).part.isSelected = true
                    //go.TextEditingTool.prototype.doActivate();
                }
            }
            salvar()
        }
        function addIrmao() {
            var obj = myDiagram.selection.first();
            if (obj != null && !obj.isTreeLink) {
                var clicked = obj.part;
                if (clicked !== null) {
                    var thisemp = clicked.data;
                    if (thisemp.parent != null && thisemp.parent !== "") {
                        myDiagram.startTransaction("Adicionar Item");
                        var nodeParent = obj.findTreeParentNode();
                        var nextkey = (myDiagram.model.nodeDataArray.length).toString();
                        var newEdt = nodeParent.data.EDT == '0' ?
                            (nodeParent.findTreeChildrenNodes().count + 1).toString() :
                            nodeParent.data.EDT + '.' + (nodeParent.findTreeChildrenNodes().count + 1);
                        var newemp = {
                            key: nextkey, name: traducao.graficoEAP_novo_item_, title: "", parent: thisemp.parent, idTarefa: "", nivel: thisemp.nivel, indicaFechado: false,
                            Inicio: "", Termino: "", ValorPeso: "0", PercentualPesoTarefa: "0", Custo: "0", Receita: "", Trabalho: "0",
                            PodeEditar: "S", PodeExcluir: "S", PodeAdicionarFilho: "S", CodigoUsuarioResponsavel: "",
                            Anotacoes: "", Duracao: "", IndicaAtribuicaoManualPesoTarefa: "N", PodeEditarPeso: "S", CodigoInternoTarefa: "-1", CriterioAceitacao: "",
                            IndicaResumo: "N", SequenciaTarefaCronograma: "", EDT: newEdt
                        };
                        myDiagram.model.addNodeData(newemp);
                        myDiagram.commitTransaction("Adicionar Item");
                        mudaCor();
                        window.parent.existeConteudoCampoAlterado = true;
                    }
                }
            }
            salvar();
        }
        function excluir() {
            var obj = myDiagram.selection.first();
            //Define que pode apagar qualquer balão da EAP da arvore exeto o primeiro balão(TarefaPaiDeTodas)
            if (obj.data.PodeExcluir === 'S' || obj.data.PodeExcluir === 'N') {
                indicaExclusao = true;
                calculaDatasArvoreEAP(obj);
                calculaPesosGeralEAP(obj, true);
                indicaExclusao = false;
                window.parent.existeConteudoCampoAlterado = true;
                if (obj != null && !obj.isTreeLink) {
                    excluiFilhos(obj);
                    if (obj.findTreeLevel() > 0) {
                        var parent = obj.findTreeParentNode();
                        myDiagram.remove(obj);
                        defineEDT(parent, true, parent.data.EDT, null);
                    }
                }
            }
            salvar()
        }
        function excluiFilhos(somenode) {
            var it = somenode.findTreeChildrenNodes();
            if (it != null) {
                while (it.next()) {
                    excluiFilhos(it.value);
                    myDiagram.remove(it.value);
                }
            }
        }
        function zoomOut() {
            if (myDiagram.scale > 0.50) {
                var scaleLevelChange = 0.10;
                // Scale the diagram and center it
                myDiagram.scale = myDiagram.scale - scaleLevelChange;
                myDiagram.centerRect(myDiagram.findTreeRoots().first().actualBounds);
            }
            salvar();
        }
        function zoomIn() {
            if (myDiagram.scale < 2.00) {
                var scaleLevelChange = 0.10;

                myDiagram.scale = myDiagram.scale + scaleLevelChange;
                myDiagram.centerRect(myDiagram.findTreeRoots().first().actualBounds);
            }
            salvar()
        }
        function getImage() {
            var img = myDiagram.makeImage({
                scale: 1,
                background: "white",
                type: "image/png",
                maxSize: new go.Size(20000, 20000)
            });

            download(img.src, "EAP.png", "image/png");
        }
        var verificaAcoes = true;
        var fonte = null;
        var indicaExclusao = false;
        var sequenciaItem = 0;
        function mudaCor() {
            sequenciaItem = 0;
            var it = myDiagram.findTreeRoots();
            if (it != null) {
                while (it.next()) {
                    var obj = it.value;
                    if (obj != null) {
                        if (obj.part.data.nivel != 0) {
                            myDiagram.remove(obj.part);
                        } else {
                            changeStyleNodes(obj);
                            configuraObjetos(obj);
                        }
                    }
                }
            }
            verificaAcoes = false;
        }
        function configuraTexto(txt, indicaVisivel, indicaEditavel) {
            if (txt) {
                txt.font = fonte + 'pt Verdana';
                txt.stroke = colorEditFonte.GetText();
                txt.visible = indicaVisivel;
                txt.editable = indicaEditavel;
            }
        }
        function changeStyleNodes(somenode) {
            var it = somenode.findTreeChildrenNodes();
            var model = myDiagram.model;
            model.startTransaction("Atualizando Sequencia");
            model.setDataProperty(somenode.data, "SequenciaTarefaCronograma", sequenciaItem);
            model.commitTransaction("Resumos Sequencia");
            sequenciaItem++;
            if (it != null) {
                var contador = 0;
                while (it.next()) {
                    contador++;
                    changeStyleNodes(it.value);
                    configuraObjetos(it.value);
                }
                model.startTransaction("Atualizando Resumo");
                model.setDataProperty(somenode.data, "IndicaResumo", contador > 0 ? 'S' : 'N');
                model.commitTransaction("Resumos Atualizados");
            } else {
                model.startTransaction("Atualizando Resumo");
                model.setDataProperty(somenode.data, "IndicaResumo", 'N');
                model.commitTransaction("Resumos Atualizados");
            }
        }
        function configuraObjetos(obj) {
            var indicaEditavel = hfGeral.Get("Acesso") && obj.data.nivel > 0;
            var isTreeLeaf = obj.isTreeLeaf;

            if (verificaAcoes)
                obj.isTreeExpanded = obj.data.indicaFechado == '0';

            var shape = obj.findObject("SHAPE");
            if (shape) shape.fill = colorEdit.GetText();

            var txtNome = obj.findObject("txtNome");
            if (txtNome) txtNome.stroke = colorEditFonte.GetText();

            var lblEdt = obj.findObject("textBlockEDT");
            configuraTexto(lblEdt, true, false);
            var textBlockItem = obj.findObject("textBlockPanel");
            configuraTexto(textBlockItem, true, indicaEditavel);
            var textBlockInicio = obj.findObject("textBlockInicio");
            configuraTexto(textBlockInicio, ddlVisao.GetValue() == 'C', false && isTreeLeaf);
            var lblInicio = obj.findObject("lblInicio");
            configuraTexto(lblInicio, ddlVisao.GetValue() == 'C', false);
            var textBlockTermino = obj.findObject("textBlockTermino");
            configuraTexto(textBlockTermino, ddlVisao.GetValue() == 'C', false && isTreeLeaf);
            var lblTermino = obj.findObject("lblTermino");
            configuraTexto(lblTermino, ddlVisao.GetValue() == 'C', false);
            var textBlockCusto = obj.findObject("textBlockCusto");
            configuraTexto(textBlockCusto, ddlVisao.GetValue() == 'C', false && isTreeLeaf);
            var lblCusto = obj.findObject("lblCusto");
            configuraTexto(lblCusto, ddlVisao.GetValue() == 'C', false);
            var textBlockReceita = obj.findObject("textBlockReceita");
            configuraTexto(textBlockReceita, ddlVisao.GetValue() == 'C', false && isTreeLeaf);
            var lblReceita = obj.findObject("lblReceita");
            configuraTexto(lblReceita, ddlVisao.GetValue() == 'C', false);
            var textBlockTrabalho = obj.findObject("textBlockTrabalho");
            configuraTexto(textBlockTrabalho, ddlVisao.GetValue() == 'C', false && isTreeLeaf);
            var lblTrabalho = obj.findObject("lblTrabalho");
            configuraTexto(lblTrabalho, ddlVisao.GetValue() == 'C', false);
            var textBlockValorPeso = obj.findObject("textBlockValorPeso");
            configuraTexto(textBlockValorPeso, ddlVisao.GetValue() == 'P', indicaEditavel);
            var lblPeso = obj.findObject("lblPeso");
            configuraTexto(lblPeso, ddlVisao.GetValue() == 'P', false);
            var textBlockPercentualPesoTarefa = obj.findObject("textBlockPercentualPesoTarefa");
            configuraTexto(textBlockPercentualPesoTarefa, ddlVisao.GetValue() == 'P', false);
            var lblPercentualPeso = obj.findObject("lblPercentualPeso");
            configuraTexto(lblPercentualPeso, ddlVisao.GetValue() == 'P', false);
        }
        function fontUp() {
            fonte++;
            mudaCor();
            salvar();
        }
        function fontDown() {
            if (fonte > 5) {
                fonte--;
                mudaCor();
            }
            salvar();
        }
        function verificaFechados() {
            var it = myDiagram.findTreeRoots();
            if (it != null) {
                while (it.next()) {
                    var obj = it.value;
                    if (obj != null) {
                        verificaFechadosFilhos(obj);
                        obj.data.indicaFechado = obj.isTreeExpanded ? '0' : '1';
                    }
                }
            }
        }
        function verificaFechadosFilhos(somenode) {
            var it = somenode.findTreeChildrenNodes();
            if (it != null) {
                while (it.next()) {
                    verificaFechadosFilhos(it.value);
                    it.value.data.indicaFechado = it.value.isTreeExpanded ? '0' : '1';
                }
            }
        }
        function calculaPesos(txt, var1, var2) {
            var obj = noSelecionado;
            var model = myDiagram.model;
            model.startTransaction("Atualizando IndicaAtribuicaoManualPesoTarefa");
            model.setDataProperty(obj.part.data, "IndicaAtribuicaoManualPesoTarefa", "S");
            model.commitTransaction("IndicaAtribuicaoManualPesoTarefa Atualizado");
            return calculaPesosArvoreEAP(var2, obj, true);
        }
        function calculaPesosArvoreEAP(var2, obj, indicaEditado) {
            var valorTotal = 0;
            if (var2 == '')
                return false;
            if (obj != null && !obj.isTreeLink) {
                var clicked = obj.part;
                if (clicked !== null) {
                    var thisemp = clicked.data;
                    var model = myDiagram.model;
                    if (thisemp.parent != null && thisemp.parent !== "") {
                        var it = obj.findTreeParentNode().findTreeChildrenNodes();
                        var varPesos = obj.findTreeParentNode().findTreeChildrenNodes();
                        if (it != null) {
                            while (it.next()) {
                                if (it.value.data.ValorPeso != "") {
                                    var valorPesoAtual = (indicaEditado && it.value == obj) ? var2 : it.value.data.ValorPeso;
                                    valorPesoAtual = replaceAll(valorPesoAtual.toString(), separadorMilhar, '').replace(',', '.');
                                    valorTotal += parseFloat(valorPesoAtual);
                                }
                            }
                            model.startTransaction("Atualizando Pesos");
                            while (varPesos.next()) {
                                if (indicaEditado || varPesos.value.data.IndicaAtribuicaoManualPesoTarefa == 'N') {
                                    if (valorTotal > 0) {
                                        var valorPesoAtual = (indicaEditado && varPesos.value == obj) ? var2 : varPesos.value.data.ValorPeso;
                                        valorPesoAtual = replaceAll(valorPesoAtual.toString(), separadorMilhar, '').replace(',', '.');
                                        model.setDataProperty(varPesos.value.data, "PercentualPesoTarefa", formatNumber((parseFloat(valorPesoAtual) / valorTotal) * 100, 1));
                                    } else
                                        model.setDataProperty(varPesos.value.data, "PercentualPesoTarefa", '0,0');
                                }
                            }
                            var dataPai = obj.findTreeParentNode().part.data;

                            if (dataPai.IndicaAtribuicaoManualPesoTarefa == 'N')
                                model.setDataProperty(dataPai, "ValorPeso", formatNumber(valorTotal));

                            model.commitTransaction("Pesos Atualizados");
                        }
                        if (obj.findTreeParentNode().part.data.IndicaAtribuicaoManualPesoTarefa == 'N')
                            calculaPesosArvoreEAP(var2, obj.findTreeParentNode(), false);
                    }
                    else {
                        if (parseFloat(var2.replace(',', '.')) != 0) {
                            model.startTransaction("Atualizando Pesos");
                            model.setDataProperty(clicked.data, "PercentualPesoTarefa", '100,0');
                            model.commitTransaction("Pesos Atualizados");
                        }
                    }
                }
            }
            return true;
        }
        function calculaPesosGeralEAP(obj, indicaEditado) {
            var valorTotal = 0;
            if (obj != null && !obj.isTreeLink) {
                var clicked = obj.part;
                if (clicked !== null) {
                    var thisemp = clicked.data;
                    var model = myDiagram.model;
                    if (thisemp.parent != null && thisemp.parent !== "") {
                        var it = obj.findTreeParentNode().findTreeChildrenNodes();
                        var varPesos = obj.findTreeParentNode().findTreeChildrenNodes();
                        if (it != null) {
                            while (it.next()) {
                                if (it.value.data.ValorPeso != "") {
                                    var valorPesoAtual = (indicaEditado && indicaExclusao && it.value == obj) ? 0 : it.value.data.ValorPeso;
                                    valorPesoAtual = replaceAll(valorPesoAtual.toString(), separadorMilhar, '').replace(',', '.');
                                    valorTotal += parseFloat(valorPesoAtual);
                                }
                            }
                            model.startTransaction("Atualizando Pesos");
                            while (varPesos.next()) {

                                if (indicaEditado || varPesos.value.data.IndicaAtribuicaoManualPesoTarefa == 'N') {
                                    if (valorTotal > 0) {
                                        var valorPesoAtual = (indicaEditado && indicaExclusao && varPesos.value == obj) ? 0 : varPesos.value.data.ValorPeso;
                                        valorPesoAtual = replaceAll(valorPesoAtual.toString(), separadorMilhar, '').replace(',', '.');
                                        model.setDataProperty(varPesos.value.data, "PercentualPesoTarefa", formatNumber((parseFloat(valorPesoAtual) / valorTotal) * 100, 1));
                                    } else
                                        model.setDataProperty(varPesos.value.data, "PercentualPesoTarefa", '0,0');
                                }
                            }
                            var dataPai = obj.findTreeParentNode().part.data;

                            if (dataPai.IndicaAtribuicaoManualPesoTarefa == 'N')
                                model.setDataProperty(dataPai, "ValorPeso", formatNumber(valorTotal));

                            model.commitTransaction("Pesos Atualizados");
                        }

                        if (obj.findTreeParentNode().part.data.IndicaAtribuicaoManualPesoTarefa == 'N')
                            calculaPesosGeralEAP(obj.findTreeParentNode(), false);
                    }
                    else {
                        if (thisemp.IndicaAtribuicaoManualPesoTarefa == 'N') {
                            model.startTransaction("Atualizando Pesos");
                            model.setDataProperty(clicked.data, "PercentualPesoTarefa", '100,0');
                            model.commitTransaction("Pesos Atualizados");
                        }
                    }
                }
            }
        }
        function defineEDT(node, forceChange, edtValue, previousParentNode) {

            if (previousParentNode) {
                var previousParentEDT = previousParentNode.data.EDT;
                var position = getNodePosition(node.data.EDT);
                previousParentNode.findTreeChildrenNodes()
                    .filter(function (n) {
                        var p = getNodePosition(n.data.EDT);
                        return p > position;
                    })
                    .each(function (n) {
                        var p = getNodePosition(n.data.EDT) - 1;
                        newEdt = (previousParentEDT === '0') ? p.toString() : previousParentEDT + '.' + p;
                        defineEDT(n, true, newEdt);
                    });
            }
            if (!(edtValue)) {
                var parent = node.findTreeParentNode();
                if (parent) {
                    var parentEDT = parent.data.EDT;
                    var quantidadeFilhos = parent.findTreeChildrenNodes().count;
                    if (parentEDT === '0')
                        edtValue = quantidadeFilhos.toString();
                    else
                        edtValue = parentEDT + '.' + quantidadeFilhos;

                    /**/
                }
            }
            if (forceChange || node.data.EDT == null || node.data.EDT == undefined || node.data.EDT == "") {
                myDiagram.model.setDataProperty(node.data, "EDT", edtValue);
                if (edtValue === '0')
                    myDiagram.model.setDataProperty(node.data, "PodeExcluir", false);
            }
            if (edtValue == '0') {
                edtValue = null;
            }
            var count = 1;
            var childrenNodes = node.findTreeChildrenNodes();
            if (childrenNodes != null && childrenNodes.count > 0) {
                while (childrenNodes.next()) {
                    var childNode = childrenNodes.value;
                    defineEDT(childNode, forceChange, ((edtValue) ? edtValue + '.' + count : count.toString()));
                    count++;
                }
            }
        }
        function getNodePosition(edtValue) {
            var index = edtValue.lastIndexOf('.');
            var position = parseInt(edtValue.substring(index + 1));
            return position;
        }
        function calculaPesosEAP(obj) {
            var valorTotal = 0;
            if (obj != null && !obj.isTreeLink) {
                var clicked = obj.part;
                if (clicked !== null) {
                    var thisemp = clicked.data;
                    var model = myDiagram.model;
                    var it = obj.findTreeChildrenNodes();
                    var varPesos = obj.findTreeChildrenNodes();
                    if (it != null && it.count > 0) {
                        while (it.next()) {
                            calculaPesosEAP(it.value);
                        }
                    }
                    else {
                        indicaExclusao = false;
                        calculaPesosGeralEAP(obj, true);
                        calculaDatasArvoreEAP(obj);
                    }
                }
            }
        }
        function validaDatas(txt, var1, var2) {
            if (!(var2)) return false;

            var data = tryParseDate(var2);
            if (data !== null) {
                var obj = txt.part.data;
                var dataInicio = (txt.name === "textBlockInicio") ? data : tryParseDate(obj.Inicio);
                var dataTermino = (txt.name === "textBlockTermino") ? data : tryParseDate(obj.Termino);

                if (dataInicio === null || dataTermino === null) return true;

                return dataInicio <= dataTermino;
            }

            return false;
        }
        function validaNumero(txt, var1, var2) {
            if (!(var2)) return false;

            return true;
        }
        function validaNome(txt, var1, var2) {
            return (!!(var2.trim()));
        }
        function tryParseDate(strDate) {
            var parts = strDate.split('/');
            if (parts.length !== 3) return null;

            var d = parseInt(parts[0]);
            var m = parseInt(parts[1]);
            var y = parseInt(parts[2]);

            m = parseInt(m, 10) - 1;
            if (m >= 0 && m < 12 && d > 0 && d <= daysInMonth(m, y))
                return new Date(y, m, d);

            return null;
        };
        function daysInMonth(m, y) {
            switch (m) {
                case 1:
                    return (y % 4 == 0 && y % 100) || y % 400 == 0 ? 29 : 28;
                case 8: case 3: case 5: case 10:
                    return 30;
                default:
                    return 31;
            }
        };
        function calculaDatas(txt, var1, var2) {
            var obj = (txt) ? txt.part : myDiagram.selection.first();
            defineDuracaoTarefa(obj.part.data);
            return calculaDatasArvoreEAP(obj);
        }
        function defineDuracaoTarefa(dados) {
            var model = myDiagram.model;
            model.startTransaction("Atualizando duracao tarefa");
            model.setDataProperty(dados, "Duracao", calculaDiasUteis(dados.Inicio, dados.Termino));
            model.commitTransaction("Duração da tarefa atualizada");
        }
        function calculaDatasArvoreEAP(obj) {
            var maiorData = null, menorData = null;
            var inicioPai, terminoPai;
            var valorTrabalho = 0, valorCusto = 0, valorReceita = 0;

            if (obj != null && !obj.isTreeLink) {
                var clicked = obj.part;
                if (clicked !== null) {
                    var thisemp = clicked.data;
                    var model = myDiagram.model;

                    if (thisemp.parent != null && thisemp.parent !== "") {
                        var objPai = obj.findTreeParentNode();

                        menorData = null;
                        maiorData = null;

                        var it = objPai.findTreeChildrenNodes();
                        var varPesos = obj.findTreeParentNode().findTreeChildrenNodes();
                        if (it != null) {
                            while (it.next()) {

                                if (it.value == obj && indicaExclusao) {
                                    if (it.count) {
                                        if (!(inicioPai)) {
                                            inicioPai = it.value.data.Inicio;
                                        }
                                        if (!(terminoPai)) {
                                            terminoPai = it.value.data.Termino;
                                        }
                                    }
                                }
                                else {
                                    if (it.value.data.Inicio != "") {
                                        var inicioAtual = getDateFormat(it.value.data.Inicio);

                                        if (inicioAtual != null) {
                                            if (menorData == null)
                                                menorData = inicioAtual;

                                            if (inicioAtual <= menorData) {
                                                menorData = inicioAtual;
                                                inicioPai = it.value.data.Inicio;
                                            }
                                        }
                                    }
                                    if (it.value.data.Termino != "") {
                                        var terminoAtual = getDateFormat(it.value.data.Termino);

                                        if (terminoAtual != null) {
                                            if (maiorData == null)
                                                maiorData = terminoAtual;

                                            if (terminoAtual >= maiorData) {
                                                maiorData = terminoAtual;
                                                terminoPai = it.value.data.Termino;
                                            }
                                        }
                                    }
                                    if (it.value.data.Trabalho != "")
                                        valorTrabalho += parseFloat(replaceAll(it.value.data.Trabalho.toString(), separadorMilhar, '').replace(',', '.'));

                                    if (it.value.data.Custo != "")
                                        valorCusto += parseFloat(replaceAll(it.value.data.Custo.toString(), separadorMilhar, '').replace(',', '.'));

                                    if (it.value.data.Receita != "")
                                        valorReceita += parseFloat(replaceAll(it.value.data.Receita.toString(), separadorMilhar, '').replace(',', '.'));
                                }
                            }
                            model.startTransaction("Atualizando EAP");
                            var dataPai = obj.findTreeParentNode().part.data;
                            /*
                            valorTrabalho = replaceAll(valorTrabalho.toString(), '.', '').replace(',', '.');
                            valorCusto = replaceAll(valorCusto.toString(), '.', '').replace(',', '.');
                            valorReceita = replaceAll(valorReceita.toString(), '.', '').replace(',', '.');
                            */
                            //if (dataPai.IndicaAtribuicaoManualPesoTarefa == 'N')
                            model.setDataProperty(dataPai, "Inicio", inicioPai);
                            model.setDataProperty(dataPai, "Termino", terminoPai);
                            model.setDataProperty(dataPai, "Duracao", calculaDiasUteis(inicioPai, terminoPai));
                            model.setDataProperty(dataPai, "Trabalho", valorTrabalho == '' ? '' : formatNumber(parseFloat(valorTrabalho), 2));
                            model.setDataProperty(dataPai, "Custo", valorCusto == '' ? '' : formatNumber(parseFloat(valorCusto)));
                            model.setDataProperty(dataPai, "Receita", valorReceita == '' ? '' : formatNumber(parseFloat(valorReceita)));

                            model.commitTransaction("EAP Atualizada");
                        }
                        indicaExclusao = false;
                        calculaDatasArvoreEAP(obj.findTreeParentNode());
                    }
                }
            }
            return true;
        }
        function calculaDiasUteis(sDate1, sDate2) { // input given as Date objects
            if (!(sDate1)) return 0;
            if (!(sDate2)) return 0;
            var funcGetDateFromString = function (sDate) {
                if (!!(culture) && culture.startsWith('en'))
                    return new Date(sDate);

                var parts = sDate.split('/');;
                return new Date(parts[2], parts[1] - 1, parts[0]);
            }
            var dDate1 = funcGetDateFromString(sDate1);
            var dDate2 = funcGetDateFromString(sDate2);
            var iWeeks, iDateDiff, iAdjust = 0;
            if (dDate2 < dDate1) return -1; // error code if dates transposed
            var iWeekday1 = dDate1.getDay(); // day of week
            var iWeekday2 = dDate2.getDay();
            iWeekday1 = (iWeekday1 == 0) ? 7 : iWeekday1; // change Sunday from 0 to 7
            iWeekday2 = (iWeekday2 == 0) ? 7 : iWeekday2;
            if ((iWeekday1 > 5) && (iWeekday2 > 5)) iAdjust = 1; // adjustment if both days on weekend
            iWeekday1 = (iWeekday1 > 5) ? 5 : iWeekday1; // only count weekdays
            iWeekday2 = (iWeekday2 > 5) ? 5 : iWeekday2;

            // calculate differnece in weeks (1000mS * 60sec * 60min * 24hrs * 7 days = 604800000)
            iWeeks = Math.floor((dDate2.getTime() - dDate1.getTime()) / 604800000)

            if (iWeekday1 <= iWeekday2) {
                iDateDiff = (iWeeks * 5) + (iWeekday2 - iWeekday1)
            } else {
                iDateDiff = ((iWeeks + 1) * 5) - (iWeekday1 - iWeekday2)
            }
            iDateDiff -= iAdjust // take into account both days on weekend
            return (iDateDiff + 1); // add 1 because dates are inclusive
        }
        function replaceAll(origem, antigo, novo) {
            var teste = 0;
            while (teste == 0) {
                if (origem.indexOf(antigo) >= 0) {
                    origem = origem.replace(antigo, novo);
                }
                else
                    teste = 1;
            }
            return origem;
        }
        function getDateFormat(st) {
            if (st != null && st != '') {
                var pattern = /(\d{2})\/(\d{2})\/(\d{4})/;
                return new Date(st.replace(pattern, indicaIdiomaPortugues ? '$3-$2-$1T00:00:00' : '$3-$1-$2T00:00:00'));
            }
            else
                return null;
        }
        function formatNumber(n, c, d, t) {
            c = isNaN(c = Math.abs(c)) ? 2 : c,
                d = d == undefined ? separadorDecimal : d,
                t = t == undefined ? separadorMilhar : t,
                s = n < 0 ? "-" : "",
                i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
                j = (j = i.length) > 3 ? j % 3 : 0;
            return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
        }
        function atualizaInformacoesEAP(nomeTarefa, inicio, termino, duracao, trabalho, despesa, receita, peso, responsavel, criterios, anotacoes, indicaPesoAlterado) {
            if (responsavel == null)
                responsavel = '';

            window.parent.existeConteudoCampoAlterado = true;
            var model = myDiagram.model;
            var noEAP = myDiagram.selection.first();
            model.startTransaction("Atualizando EAP");
            model.setDataProperty(noEAP.data, "name", nomeTarefa);
            model.setDataProperty(noEAP.data, "Inicio", inicio);
            model.setDataProperty(noEAP.data, "Termino", termino);
            model.setDataProperty(noEAP.data, "Duracao", duracao);
            model.setDataProperty(noEAP.data, "Trabalho", trabalho);
            model.setDataProperty(noEAP.data, "Custo", despesa);
            model.setDataProperty(noEAP.data, "Receita", receita);
            model.setDataProperty(noEAP.data, "Peso", peso);
            model.setDataProperty(noEAP.data, "ValorPeso", peso);
            model.setDataProperty(noEAP.data, "IndicaAtribuicaoManualPesoTarefa", indicaPesoAlterado ? "S" : "N");
            model.setDataProperty(noEAP.data, "CodigoUsuarioResponsavel", responsavel);
            model.setDataProperty(noEAP.data, "CriterioAceitacao", criterios);
            model.setDataProperty(noEAP.data, "Anotacoes", anotacoes);
            model.commitTransaction("EAP Atualizada");
            calculaDatas();
            calculaPesosArvoreEAP(peso, noEAP, true);
        }
        function atualizaDadosPopup(nomeTarefa, inicio, termino, duracao, trabalho, despesa, receita, peso, responsavel, criterios, anotacoes) {
            var obj = myDiagram.selection.first();
            var clicked = obj.part.data;
            nomeTarefa.SetText(clicked.name);
            inicio.SetText(clicked.Inicio);
            termino.SetText(clicked.Termino);
            duracao.SetText(clicked.Duracao + "");
            trabalho.SetText(clicked.Trabalho);
            despesa.SetText(clicked.Custo);
            receita.SetText(clicked.Receita);
            peso.SetText(clicked.ValorPeso);
            if (clicked.CodigoUsuarioResponsavel)
                responsavel.SetValue(clicked.CodigoUsuarioResponsavel.toString());
            //responsavel.SetText(responsavel.cp_NomeResponsavel);
            criterios.SetText(clicked.CriterioAceitacao == null ? '' : clicked.CriterioAceitacao);
            anotacoes.SetText(clicked.Anotacoes == null ? '' : clicked.Anotacoes);
        }
        //download.js v4.1, by dandavis; 2008-2015. [CCBY2] see http://danml.com/download.html for tests/usage
        // v1 landed a FF+Chrome compat way of downloading strings to local un-named files, upgraded to use a hidden frame and optional mime
        // v2 added named files via a[download], msSaveBlob, IE (10+) support, and window.URL support for larger+faster saves than dataURLs
        // v3 added dataURL and Blob Input, bind-toggle arity, and legacy dataURL fallback was improved with force-download mime and base64 support. 3.1 improved safari handling.
        // v4 adds AMD/UMD, commonJS, and plain browser support
        // v4.1 adds url download capability via solo URL argument (same domain/CORS only)
        // https://github.com/rndme/download
        (function (root, factory) {
            if (typeof define === 'function' && define.amd) {
                // AMD. Register as an anonymous module.
                define([], factory);
            } else if (typeof exports === 'object') {
                // Node. Does not work with strict CommonJS, but
                // only CommonJS-like environments that support module.exports,
                // like Node.
                module.exports = factory();
            } else {
                // Browser globals (root is window)
                root.download = factory();
            }
        }(this, function () {
            return function download(data, strFileName, strMimeType) {
                var self = window, // this script is only for browsers anyway...
                    u = "application/octet-stream", // this default mime also triggers iframe downloads
                    m = strMimeType || u,
                    x = data,
                    url = !strFileName && !strMimeType && x,
                    D = document,
                    a = D.createElement("a"),
                    z = function (a) { return String(a); },
                    B = (self.Blob || self.MozBlob || self.WebKitBlob || z),
                    fn = strFileName || "download",
                    blob,
                    fr,
                    ajax;
                B = B.call ? B.bind(self) : Blob;
                if (String(this) === "true") { //reverse arguments, allowing download.bind(true, "text/xml", "export.xml") to act as a callback
                    x = [x, m];
                    m = x[0];
                    x = x[1];
                }
                if (url && url.length < 2048) {
                    fn = url.split("/").pop().split("?")[0];
                    a.href = url; // assign href prop to temp anchor
                    if (a.href.indexOf(url) !== -1) { // if the browser determines that it's a potentially valid url path:
                        var ajax = new XMLHttpRequest();
                        ajax.open("GET", url, true);
                        ajax.responseType = 'blob';
                        ajax.onload = function (e) {
                            download(e.target.response, fn, u);
                        };
                        ajax.send();
                        return ajax;
                    } // end if valid url?
                } // end if url?
                //go ahead and download dataURLs right away
                if (/^data\:[\w+\-]+\/[\w+\-]+[,;]/.test(x)) {
                    return navigator.msSaveBlob ?  // IE10 can't do a[download], only Blobs:
                        navigator.msSaveBlob(d2b(x), fn) :
                        saver(x); // everyone else can save dataURLs un-processed
                }//end if dataURL passed?

                blob = x instanceof B ?
                    x :
                    new B([x], { type: m });

                function d2b(u) {
                    var p = u.split(/[:;,]/),
                        t = p[1],
                        dec = p[2] == "base64" ? atob : decodeURIComponent,
                        bin = dec(p.pop()),
                        mx = bin.length,
                        i = 0,
                        uia = new Uint8Array(mx);

                    for (i; i < mx; ++i) uia[i] = bin.charCodeAt(i);

                    return new B([uia], { type: t });
                }
                function saver(url, winMode) {
                    if ('download' in a) { //html5 A[download]
                        a.href = url;
                        a.setAttribute("download", fn);
                        a.className = "download-js-link";
                        a.innerHTML = "downloading...";
                        D.body.appendChild(a);
                        setTimeout(function () {
                            a.click();
                            D.body.removeChild(a);
                            if (winMode === true) { setTimeout(function () { self.URL.revokeObjectURL(a.href); }, 250); }
                        }, 66);
                        return true;
                    }
                    if (typeof safari !== "undefined") { // handle non-a[download] safari as best we can:
                        url = "data:" + url.replace(/^data:([\w\/\-\+]+)/, u);
                        if (!window.open(url)) { // popup blocked, offer direct download:
                            if (confirm("Displaying New Document\n\nUse Save As... to download, then click back to return to this page.")) { location.href = url; }
                        }
                        return true;
                    }
                    //do iframe dataURL download (old ch+FF):
                    var f = D.createElement("iframe");
                    D.body.appendChild(f);
                    if (!winMode) { // force a mime that will download:
                        url = "data:" + url.replace(/^data:([\w\/\-\+]+)/, u);
                    }
                    f.src = url;
                    setTimeout(function () { D.body.removeChild(f); }, 333);

                }//end saver
                if (navigator.msSaveBlob) { // IE10+ : (has Blob, but not a[download] or URL)
                    return navigator.msSaveBlob(blob, fn);
                }
                if (self.URL) { // simple fast and modern way using Blob and URL:
                    saver(self.URL.createObjectURL(blob), true);
                } else {
                    // handle non-Blob()+non-URL browsers:
                    if (typeof blob === "string" || blob.constructor === z) {
                        try {
                            return saver("data:" + m + ";base64," + self.btoa(blob));
                        } catch (y) {
                            return saver("data:" + m + "," + encodeURIComponent(blob));
                        }
                    }
                    // Blob but not URL:
                    fr = new FileReader();
                    fr.onload = function (e) {
                        saver(this.result);
                    };
                    fr.readAsDataURL(blob);
                }
                return true;
            }; /* end download() */
        }));
        function somenteNumeros(obj, e) {
            var tecla = (window.event) ? e.keyCode : e.which;

            if (tecla == 44 && obj.value.indexOf(',') > -1)
                return false;

            if (tecla == 8 || tecla == 0)
                return true;
            if (tecla != 44 && tecla < 48 || tecla > 57)
                return false;
        }
        function somenteData(obj, e) {
            var tecla = (window.event) ? e.keyCode : e.which;

            if (tecla == 44 && obj.value.indexOf(',') > -1)
                return false;

            if (tecla == 47 || tecla == 8 || tecla == 0)
                return true;
            if (tecla != 44 && tecla < 48 || tecla > 57)
                return false;
        }
        var refreshinterval = 120;
        var starttime;
        var nowtime;
        var reloadseconds = 0;
        var secondssinceloaded = 0;
        function starttime() {
            starttime = new Date();
            starttime = starttime.getTime();
            countdown();
        }
        function countdown() {
            nowtime = new Date();
            nowtime = nowtime.getTime();
            secondssinceloaded = (nowtime - starttime) / 1000;
            reloadseconds = Math.round(refreshinterval - secondssinceloaded);
            if (refreshinterval >= secondssinceloaded) {
                var timer = setTimeout("countdown()", 2);

            }
            else {
                clearTimeout(timer);
                callbackSession.PerformCallback();
            }
        }
        function ajustaLayout() {
            myDiagram.startTransaction("change Layout");
            var lay = myDiagram.layout;
            var opcao = parseInt(ddlLayout.GetValue());
            lay.arrangement = go.TreeLayout.ArrangementFixedRoots;
            lay.layerSpacing = 35;
            switch (opcao) {
                case 0:
                    lay.angle = 90;
                    lay.treeStyle = go.TreeLayout.StyleLayered;
                    lay.alignment = go.TreeLayout.AlignmentCenterChildren;
                    myDiagram.contentAlignment = go.Spot.TopCenter;
                    break;
                case 1:
                    lay.angle = 0;
                    lay.treeStyle = go.TreeLayout.StyleLayered;
                    lay.alignment = go.TreeLayout.AlignmentStart;
                    myDiagram.contentAlignment = go.Spot.TopLeft;
                    break;
                case 2:
                    lay.angle = 90;
                    lay.treeStyle = go.TreeLayout.StyleLastParents;
                    lay.alignment = go.TreeLayout.AlignmentCenterChildren;
                    // properties for the "last parents":
                    lay.alternateAngle = 0;
                    lay.alternateAlignment = go.TreeLayout.AlignmentStart;
                    lay.alternateNodeIndent = 20;
                    lay.alternateNodeIndentPastParent = 1;
                    lay.alternateNodeSpacing = 20;
                    lay.alternateLayerSpacing = 30;
                    lay.alternateLayerSpacingParentOverlap = 1;
                    lay.alternatePortSpot = new go.Spot(0.001, 1, 20, 0);
                    lay.alternateChildPortSpot = go.Spot.Left;
                    myDiagram.contentAlignment = go.Spot.TopCenter;
                    break;
                default:
                    break;
            }
            myDiagram.commitTransaction("change Layout");
        }
        //início Modal Migração da NovaCdis.master
        function mostraMensagemEAP(textoMsg, nomeImagem, mostraBtnOK, mostraBtnCancelar, eventoOK, timeout) {
            if (!(timeout)) {
                timeout = 1500;
            }
            //lpAguardeMasterPage.Hide();
            if (nomeImagem != null && nomeImagem != '')
                imgApresentacaoAcao.SetImageUrl(pcApresentacaoAcao.cp_Path + '../imagens/' + nomeImagem + '.png');
            else
                imgApresentacaoAcao.SetVisible(false);

            textoMsg = replaceAll(textoMsg, '\n', '<br/>');

            lblMensagemApresentacaoAcao.SetText(textoMsg);
            btnOkApresentacaoAcao.SetVisible(mostraBtnOK);
            btnCancelarApresentacaoAcao.SetVisible(mostraBtnCancelar);
            pcApresentacaoAcao.Show();
            eventoOKMsg = eventoOK;
            eventoCancelarMsg = null;

            if (!mostraBtnOK && !mostraBtnCancelar) {
                setTimeout('fechaMensagemEAP();', timeout);
            }
        }
        function mostraSobre(caminho) {
            showModal(caminho, "About", 532, 330, "", null);
        }
        function showModal(sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam) {
            alert((sUrl, sHeaderTitulo, sWidth, sHeight, sFuncaoPosModal, objParam));
            if (parseInt(sHeight) < 535)
                sHeight = parseInt(sHeight) + 20;
            sWidth = sWidth <= 400 ? 900 : sWidth;

            myObject = objParam;
            posExecutar = sFuncaoPosModal != "" ? sFuncaoPosModal : null;

            pcApresentacaoAcao.SetWidth(sWidth);
            pcApresentacaoAcao.SetWidth(sWidth);
            pcApresentacaoAcao.SetHeight(sHeight + 50);
            pcApresentacaoAcao.SetContentUrl(sUrl);
            //setTimeout ('alteraUrlModal();', 0);            
            pcApresentacaoAcao.SetHeaderText(sHeaderTitulo);
            pcApresentacaoAcao.Show();
        }
        function atualizaURLModal(sUrl) {
            urlModal = sUrl;
            pcApresentacaoAcao.SetContentUrl(sUrl);
        }
        function fechaModal() {
            pcApresentacaoAcao.Hide();
        }
        function fechaMensagemEAP() {
            pcApresentacaoAcao.Hide();
        }
        function resetaModal() {
            posExecutar = null;
            pcApresentacaoAcao.SetContentUrl(pcApresentacaoAcao.cp_Path + "branco.htm");
            pcApresentacaoAcao.SetHeaderText("");
            retornoModal = null;
        }
        function fechaModal2() {
            //pcModal2.SetContentUrl(pcModal.cp_Path + "branco.htm");
            pcApresentacaoAcao.Hide();
        }
        function resetaModal2() {
            posExecutar2 = null;
            pcApresentacaoAcao.SetContentUrl(pcModal.cp_Path + "branco.htm");
            pcApresentacaoAcao.SetHeaderText("");
            retornoModal2 = null;
        }
        //fim Modal Migração da NovaCdis.master
    </script>
    <style type="text/css">
        .btn_inicialMaiuscula {
            text-transform: capitalize !important;
        }
    </style>
</head>
<body onload="trataInit()">
    <form id="form1" runat="server">
        <input id="retorno_popup" name="retorno_popup" runat="server" type="hidden" value="" />
        <%-- <div id="sample">--%>
        <div id="toolBar" runat="server" style="padding: 5px; border: 1px solid #bfbebe; border-bottom: none">

            <table cellspacing="0" class="dxflInternalEditorTable">
                <tr>
                    <td>

                        <table cellspacing="0" class="tabelaBotoes">
                            <tr>
                                <% if (modoAcessoDesejado == "G")
                                    {%>
                                <td id="tdSalvar" title="<%: Resources.traducao.salvar %>">
                                    <img style="cursor: pointer; padding-right: 3px; width: 30px; height: 25px; border-right: solid 1px #bfbebe;" alt="<%: Resources.traducao.salvar %>" src="../../imagens/WBSTools/save.png" id="SaveButton" runat="server" onclick="save()" />
                                </td>
                                <%} %>
                                <td title="<%: Resources.traducao.gerar_imagem %>">
                                    <img alt="<%: Resources.traducao.gerar_imagem %>" style="cursor: pointer; padding-right: 3px; border-right: solid 1px #bfbebe;" src="../../imagens/WBSTools/photo.png" onclick="getImage()" title="<%: Resources.traducao.gerar_imagem %>" /></td>
                                <% if (modoAcessoDesejado == "G")
                                    {%>
                                <td title="<%: Resources.traducao.desfazer %>">
                                    <img alt="<%: Resources.traducao.desfazer %>" style="cursor: pointer; padding-right: 3px; border-right: solid 1px #bfbebe;" src="../../imagens/WBSTools/defazer.png" onclick="undo()" title="<%: Resources.traducao.desfazer %>" />
                                </td>
                                <td title="<%: Resources.traducao.refazer %>">
                                    <img alt="<%: Resources.traducao.refazer %>" style="cursor: pointer; padding-right: 3px; border-right: solid 1px #bfbebe;" src="../../imagens/WBSTools/refazer.png" onclick="redo()" title="<%: Resources.traducao.refazer %>" />
                                </td>
                                <td id="tdAddFilho" title="<%: Resources.traducao.adicionar_filho %>">
                                    <img alt="<%: Resources.traducao.adicionar_filho %>" style="cursor: pointer; padding-right: 3px; border-right: solid 1px #bfbebe;" src="../../imagens/WBSTools/AddFilho2.png" onclick="addFilho()" title="<%: Resources.traducao.adicionar_filho %>" />
                                </td>
                                <td id="tdAddIrmao" title="<%: Resources.traducao.adicionar_irm_o %>">
                                    <img alt="<%: Resources.traducao.adicionar_irm_o %>" style="cursor: pointer; padding-right: 3px; border-right: solid 1px #bfbebe;" src="../../imagens/WBSTools/AddIrmao2.png" onclick="addIrmao()" title="<%: Resources.traducao.adicionar_irm_o %>" />
                                </td>
                                <td id="tdExcluir" title="<%: Resources.traducao.excluir %>">
                                    <img alt="<%: Resources.traducao.excluir %>" style="cursor: pointer; padding-right: 3px; border-right: solid 1px #bfbebe;" src="../../imagens/WBSTools/borrar.png" onclick="excluir()" title="<%: Resources.traducao.excluir %>" />
                                </td>
                                <%} %>
                                <td title="<%: Resources.traducao.diminuir_zoom %>">
                                    <img alt="<%: Resources.traducao.diminuir_zoom %>" style="cursor: pointer; padding-right: 3px; border-right: solid 1px #bfbebe;" src="../../imagens/WBSTools/ZoomOut.png" onclick="zoomOut()" title="<%: Resources.traducao.diminuir_zoom %>" /></td>
                                <td title="<%: Resources.traducao.aumentar_zoom %>">
                                    <img alt="<%: Resources.traducao.aumentar_zoom %>" style="cursor: pointer; padding-right: 3px; border-right: solid 1px #bfbebe;" src="../../imagens/WBSTools/ZoomIn.png" onclick="zoomIn()" title="<%: Resources.traducao.aumentar_zoom %>" /></td>
                                <td title="<%: Resources.traducao.diminuir_fonte%>">
                                    <img alt="<%: Resources.traducao.diminuir_fonte%>" style="cursor: pointer; padding-right: 3px; border-right: solid 1px #bfbebe;" src="../../imagens/WBSTools/fontDown.png" onclick="fontDown()" title="<%: Resources.traducao.diminuir_fonte%>" /></td>
                                <td title="<%: Resources.traducao.aumentar_fonte%>">
                                    <img alt="<%: Resources.traducao.aumentar_fonte%>" style="cursor: pointer; padding-right: 3px; border-right: solid 1px #bfbebe;" src="../../imagens/WBSTools/fontUp.png" onclick="fontUp()" title="<%: Resources.traducao.aumentar_fonte%>" /></td>
                                <td aria-orientation="horizontal" style="padding-left: 3px; text-align: right;">
                                    <dxcp:ASPxColorEdit ID="colorEdit" runat="server" ClientInstanceName="colorEdit" Width="38px" ColorIndicatorWidth="22px" Spacing="0" ToolTip="Cor das caixas">
                                        <ClientSideEvents ColorChanged="function(s, e) {
	mudaCor(); salvar();
}" />
                                        <Paddings Padding="0px" />
                                    </dxcp:ASPxColorEdit>
                                </td>
                                <td style="padding-left: 3px">
                                    <dxcp:ASPxColorEdit ID="colorEditFonte" runat="server" ClientInstanceName="colorEditFonte" Width="38px" ColorIndicatorWidth="22px" Spacing="0" ToolTip="Cor da fonte">
                                        <ClientSideEvents ColorChanged="function(s, e) {
	mudaCor(); salvar();
}" />
                                        <Paddings Padding="0px" />
                                    </dxcp:ASPxColorEdit>
                                </td>
                                <td style="padding-left: 10px">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td align="right">
                        <table cellspacing="0">
                            <tr>
                                <td style="padding-right: 2px">
                                    <dxcp:ASPxLabel ID="ASPxLabel2" runat="server" Text="Layout:">
                                    </dxcp:ASPxLabel>
                                </td>
                                <td style="padding-right: 2px">
                                    <dxcp:ASPxComboBox ID="ddlLayout" ClientInstanceName="ddlLayout" runat="server" Width="160px" SelectedIndex="0" OnSelectedIndexChanged="ddlLayout_SelectedIndexChanged">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ajustaLayout(); salvar();
}" />
                                        <Items>
                                            <dxtv:ListEditItem Text="Padrão" Value="0" Selected="True" />
                                            <dxtv:ListEditItem Text="Aberta" Value="1" />
                                            <dxtv:ListEditItem Text="Último nível aberto" Value="2" />
                                        </Items>
                                    </dxcp:ASPxComboBox>
                                </td>
                                <td style="padding-right: 2px; padding-left: 10px;">
                                    <dxcp:ASPxLabel ID="ASPxLabel1" runat="server" Text="Visão:">
                                    </dxcp:ASPxLabel>
                                </td>
                                <td>
                                    <dxcp:ASPxComboBox ID="ddlVisao" ClientInstanceName="ddlVisao" runat="server" Width="160px" OnSelectedIndexChanged="ddlVisao_SelectedIndexChanged">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	mudaCor(); salvar();
}" />
                                        <Items>
                                            <dxtv:ListEditItem Text="Simples" Value="S" />
                                            <dxtv:ListEditItem Text="Custo e Prazo" Value="C" />
                                            <dxtv:ListEditItem Text="Pesos" Value="P" />
                                        </Items>
                                    </dxcp:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">

                        <dxcp:ASPxLabel ID="lblAviso" runat="server" Text="<%$ Resources:traducao,lblAviso %>" ClientInstanceName="lblAviso" ClientVisible="False" Font-Italic="True" ForeColor="Red">
                        </dxcp:ASPxLabel>

                    </td>
                </tr>
            </table>
        </div>
        <div id="containerMyDiagram" style="display:flex;flex-direction:column">
            <div id="myDiagramDiv" style="background-color: white; border: solid 1px #bfbebe;"></div>
            <div id="divBotaoFechar">
                <textarea id="mySavedModel" runat="server" style="width: 100%; display: none">
    </textarea>
                <table style="margin-left: auto; padding-top: 5px; position: fixed; bottom: 0px; right: 0px;z-index:625">
                    <tr>
                        <td>
                            <dxe:ASPxButton runat="server" ClientInstanceName="btnCancelar" Text="Fechar" Width="100px"
                                ID="btnCancelar" CssClass="btn_inicialMaiuscula">
                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = false;
                                    		var codigo =  hfGeral.Get('IDEdicaoEAP');
                                document.getElementById('retorno_popup').value = codigo;
	window.top.fechaModal();
}"></ClientSideEvents>
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>



        <%--        </div>--%>
        <dxcp:ASPxCallback ID="callbackSalvar" runat="server" ClientInstanceName="callbackSalvar" OnCallback="callbackSalvar_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	if(s.cp_Msg != '')
	{
		var codigo = s.cpCodigo;
                                document.getElementById('retorno_popup').value = codigo;

                                 if(s.cp_status == 'ok')
            			window.top.mostraMensagem(s.cp_msg, 'sucesso', false, false, null);
        		else
            			window.top.mostraMensagem(s.cp_msg, 'erro', true, false, null);
	}
}" />
        </dxcp:ASPxCallback>
        <dxcp:ASPxHiddenField ID="hfGeral" runat="server" ClientInstanceName="hfGeral">
        </dxcp:ASPxHiddenField>
        <dxcb:ASPxCallback ID="callbackSession" runat="server" ClientInstanceName="callbackSession">
            <ClientSideEvents EndCallback="function(s, e) {
	            starttime = new Date();
                starttime = starttime.getTime();
                countdown();
}" />
        </dxcb:ASPxCallback>

        <input type="text" id='customTextEditor' class="idClasscustomTextEditor" maxlength="17" onkeypress='return somenteNumeros(this, event)' style='position: absolute; visibility: hidden; outline: none; autocapitalize: on; width: 80px'></input>

        <input type="text" id='customDateEditor' maxlength="10" placeholder="dd/mm/aaaa" onkeypress='return somenteData(this, event)' style='position: absolute; visibility: hidden; outline: none; autocapitalize: on; width: 80px'></input>



        <%--Início PopUpMensagem Migrado NovaCDISmaster implantação em paineis--%>
        <dxtv:ASPxPopupControl ID="pcApresentacaoAcao" runat="server" ClientInstanceName="pcApresentacaoAcao" HeaderText="Incluir a Entidad Atual" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False" MaxWidth="650px" MinWidth="260px" Modal="True" CloseAction="None">
            <ClientSideEvents Closing="function(s, e) {
	lblMensagemApresentacaoAcao.SetText('');
}"
                Shown="function(s, e) {
	lpAguardeMasterPage.Hide();
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
                                                        fechaMensagemEAP();
	if(eventoOKMsg != null &amp;&amp; eventoOKMsg != '')
		eventoOKMsg();
	
}" />
                                                <Paddings Padding="0px" />
                                            </dxtv:ASPxButton>
                                        </td>
                                        <td style="padding-left: 3px">
                                            <dxtv:ASPxButton ID="btnCancelarApresentacaoAcao" runat="server" AutoPostBack="False" Text="Cancelar" Width="70px" ClientInstanceName="btnCancelarApresentacaoAcao">
                                                <ClientSideEvents Click="function(s, e) {
                                                        
	fechaMensagemEAP();
	if(eventoCancelarMsg != null &amp;&amp; eventoCancelarMsg != '')
		eventoCancelarMsg();
}" />
                                                <Paddings Padding="0px" />
                                            </dxtv:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxtv:PopupControlContentControl>
            </ContentCollection>

        </dxtv:ASPxPopupControl>
        <dxcp:ASPxLoadingPanel ID="lpAguardeMasterPage" runat="server" ClientInstanceName="lpAguardeMasterPage"
            Font-Bold="True" Height="80px" HorizontalAlign="Center"
            Modal="True" Text="Aguarde&amp;hellip;" VerticalAlign="Middle" Width="200px">
        </dxcp:ASPxLoadingPanel>
        <%--fim PopUpMensagem Migrado NovaCDISmaster implantação em paineis--%>
    </form>
    <script language="javascript" type="text/javascript">
        try {
            retornoModal = hfGeral.Get("IDEdicaoEAP");
        } catch (e) {
        }
    </script>

    <script>
        var culture = '<%= System.Globalization.CultureInfo.CurrentCulture.Name %>';
        var indicaIdiomaPortugues = culture == "pt-BR";
        var mask = indicaIdiomaPortugues ? "00.000.000.000,00" : "00,000,000,000.00";
        var separadorDecimal = indicaIdiomaPortugues ? ',' : '.';
        var separadorMilhar = indicaIdiomaPortugues ? '.' : ',';
        $('.idClasscustomTextEditor').mask(mask, { reverse: true });

        //Inínio Controle Botão Salvar
        function salvar() {

            document.getElementById('SaveButton').src = "../../imagens/WBSTools/salvar.png";
            document.getElementById('SaveButton').style.width = "35px";
            document.getElementById('SaveButton').style.height = "30px";
        };

        function saveBT() {
            document.getElementById('SaveButton').src = "../../imagens/WBSTools/save.png";
            document.getElementById('SaveButton').style.width = "30px";
            document.getElementById('SaveButton').style.height = "25px";
            //traducao.eap_salva

            mostraMensagemEAP(traducao.graficoEAP_eap_salva, "sucesso", false, false, 30);
        };
        //Fim Inínio Controle Botão Salvar

        $("#other").click(function () {
            $("#target").select();
        });

        var sHeight = Math.max(0, document.documentElement.clientHeight) - 140;
        var sWidth = Math.max(0, document.documentElement.clientWidth);
        document.getElementById("myDiagramDiv").style.height = sHeight + "px";
        document.getElementById("myDiagramDiv").style.width = sWidth + "px";
        document.getElementById("myDiagramDiv").style.overflow = "auto";
    </script>
</body>
</html>
