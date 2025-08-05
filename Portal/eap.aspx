<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eap.aspx.cs" Inherits="eap" %>

<!--
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
-->
<!doctype html>
<html>
<head>
    <meta charset="UTF-8">
    <title>GoJs-Basic</title>
    <!-- Copyright 1998-2015 by Northwoods Software Corporation. -->
    <style type="text/css">
        #myOverview
        {
            position: absolute;
            top: 10px;
            left: 10px;
            background-color: aliceblue;
            z-index: 300; /* make sure its in front */
            border: solid 1px blue;
            width: 200px;
            height: 100px;
        }
        .style2
        {
            width: 83px;
        }
        .style3
        {
            width: 102px;
        }
        .style4
        {
            width: 7px;
        }
        .style5
        {
            width: 61px;
        }
    </style>
    <script src=".\scripts\go.js" type="text/javascript"> </script>
    <link href=".\estilos\goSamples.css" rel="stylesheet" type="text/css" />
    <!-- you don't need to use this -->
    <script src=".\scripts\goSamples.js" type="text/javascript"></script>
    <!-- this is only for the GoJS Samples framework -->
    <script id="code" type="text/javascript">
        var nodeDataArray;
        var $ = go.GraphObject.make;  // for conciseness in defining templates

        function init2() {
            cbEAP.PerformCallback('');
        }


        function changeColor(e, obj) {
            myDiagram.startTransaction("changed color");
            // get the context menu that holds the button that was clicked
            var contextmenu = obj.part;
            // get the node data to which the Node is data bound
            var nodedata = contextmenu.data;
            // compute the next color for the node
            var newcolor = "lightblue";
            switch (nodedata.color) {
                case "lightblue": newcolor = "lightgreen"; break;
                case "lightgreen": newcolor = "lightyellow"; break;
                case "lightyellow": newcolor = "orange"; break;
                case "orange": newcolor = "lightblue"; break;
            }
            // modify the node data
            // this evaluates data Bindings and records changes in the UndoManager
            myDiagram.model.setDataProperty(nodedata, "color", newcolor);
            myDiagram.commitTransaction("changed color");
        }

        function empty(e, obj) {
            var i = 1;
        }

        function onCallbackComplete(s, e) {
            nodeDataArray = eval(e.result.replace('"tarefaPai":null,', ''))
            init();
        }

//        function init(nodeDataArray) {
        function init() {
            //if (window.goSamples) goSamples();  // init for these samples -- you don't need to call this

            myDiagram =
      $(go.Diagram, "myDiagram",  // the DIV HTML element
        {
        initialDocumentSpot: go.Spot.TopCenter,
        initialViewportSpot: go.Spot.TopCenter,
        layout:  // create a TreeLayout
            $(go.TreeLayout,
              {
                  treeStyle: go.TreeLayout.StyleLastParents,
                  angle: 90,
                  layerSpacing: 30,
                  alternateAngle: 0,
                  alternateAlignment: go.TreeLayout.AlignmentStart,
                  alternateNodeIndent: 20,
                  alternateNodeIndentPastParent: 1,
                  alternateNodeSpacing: 20,
                  alternateLayerSpacing: 40,
                  alternateLayerSpacingParentOverlap: 1,
                  alternatePortSpot: new go.Spot(0, 0.999, 20, 0),
                  alternateChildPortSpot: go.Spot.Left
              })
    });

            // define Converters to be used for Bindings
            function theNationFlagConverter(nation) {
                return "http://www.nwoods.com/go/Flags/" + nation.toLowerCase().replace(/\s/g, "-") + "-flag.Png";
            }

            function theInfoTextConverter(info) {
                var str = "";
                if (info.dtInicio) str += "\nData Início: " + info.dtInicio;
                if (info.dtFim) str += "\nData Término: " + info.dtFim;
                return str;
            }

            // define the Node template
            myDiagram.nodeTemplate =
      $(go.Node, "Auto",
        { isShadowed: true },

        {
            doubleClick:  // here the second argument is this object, which is this Node
                function (e, node) {
                    //                    hfKey.Set("key", node.data.key);
                    document.getElementById('hfKey').value = node.data.key;
                    tbNomeTarefa.SetText(node.data.NomeTarefa);
                    deDataInicio.SetText(node.data.dtInicio);
                    deDataTermino.SetText(node.data.dtFim);
                    pcEditaTarefa.Show();
                    node.data.Refresh;
                }
        },

            //   { click: function (e, obj) { alert("teste"); } }, //alert(xnode[0]

            /*        
            { mouseEnter: function(e, obj, prev) {  // change group's background brush
            //            var xnode = obj.part.data.key;
            alert(obj);            
            if (obj) obj.fill = "green";
            }},
            { mouseLeave: function(e, obj, next) {  // restore to original brush
            //            var xnode = obj.part.keys();
            if (obj) obj.fill = "azure";
            }},
            */
            // the outer shape for the node, surrounding the Table
        $(go.Shape, "Rectangle",
          { fill: "azure" }),
            // a table to contain the different parts of the node
        $(go.Panel, "Table",
          { margin: 2, maxSize: new go.Size(200, NaN) },
            // the two TextBlocks in column 0 both stretch in width
            // but align on the left side
          $(go.RowColumnDefinition,
            {
                column: 0,
                stretch: go.GraphObject.Horizontal,
                alignment: go.Spot.Left
            }),
            // the name
          $(go.TextBlock,
            {
                row: 0, column: 0,
                maxSize: new go.Size(190, NaN),
                margin: 2,
                font: "bold 9pt sans-serif",
                alignment: go.Spot.Top
            },
            new go.Binding("text", "NomeTarefa")),
            // the country flag
            /*          $(go.Picture,
            {
            row: 0, column: 1, margin: 2,
            desiredSize: new go.Size(10, 10),
            imageStretch: go.GraphObject.Uniform,
            alignment: go.Spot.TopRight
            },
            new go.Binding("source", "dtInicio", theNationFlagConverter)),
            */
            // the additional textual information
          $(go.TextBlock,
            {
                row: 1, column: 0, columnSpan: 2,
                font: "8pt sans-serif"
            },
            new go.Binding("text", "", theInfoTextConverter)),


              {
                  contextMenu:     // define a context menu for each node
                  $(go.Adornment, "Vertical",  // that has one button

                    $("ContextMenuButton",
                      $(go.TextBlock, "---------------------------------------"),
                      { click: empty }),

                    $("ContextMenuButton",
                      $(go.TextBlock, "Change Color"),
                      { click: changeColor }),

                    $("ContextMenuButton",
                      $(go.TextBlock, "Muda Cor"),
                      { click: changeColor }),

                    $("ContextMenuButton",
                      $(go.TextBlock, "---------------------------------------"),
                      { click: empty })

                  // more ContextMenuButtons would go here
                  )  // end Adornment
              }
            /*
            $(go.TextBlock,
            {
            row: 2, column: 0,
            font: "italic 10pt verdana"
            },
            new go.Binding("text", "dtInicio"))
            */
        )  // end Table Panel

      );        // end Node

            // define the Link template, a simple orthogonal line
            myDiagram.linkTemplate =
             $(go.Link, go.Link.Orthogonal,
              { selectable: true },
              $(go.Shape, { stroke: 'red' }));  // the default black link shape


            // create the Model with data for the tree, and assign to the Diagram
            myDiagram.model =
              $(go.TreeModel,
                { nodeParentKeyProperty: "tarefaPai",  // this property refers to the parent node data
                    nodeDataArray: nodeDataArray
                });


                    // Overview
             myOverview =
              $(go.Overview, "myOverview",  // the HTML DIV element for the Overview
                {observed: myDiagram });   // tell it which Diagram to show and pan

                    //           myDiagram.addDiagramListener("ObjectDoubleClicked", function (ev, node) {
                    //               alert("Clicked on " + node.data.key);
                    //            pcEditaTarefa.Show();
                    //            //alert(ev); //Successfully logs the node you clicked.
                    //            //alert(ev.subject.ie); //Successfully logs the node's name.
                    //        }
                    //        );
         }


         // This is a dummy context menu for the whole Diagram:
         myDiagram.contextMenu = $(go.Adornment);

         // This is the actual HTML context menu:
         var cxElement = document.getElementById("contextMenu");

         // We don't want the div acting as a context menu to have a (browser) context menu!
         cxElement.addEventListener("contextmenu", function (e) { e.preventDefault(); return false; }, false);
         cxElement.addEventListener("blur", function (e) { cxMenu.stopTool(); }, false);

         // Override the ContextMenuTool.showContextMenu and hideContextMenu methods
         // in order to modify the HTML appropriately.
         var cxTool = myDiagram.toolManager.contextMenuTool;

         // This is the override of ContextMenuTool.showContextMenu:
         // This does not not need to call the base method.
         cxTool.showContextMenu = function (contextmenu, obj) {
             var diagram = this.diagram;
             if (diagram === null) return;

             // Hide any other existing context menu
             if (contextmenu !== this.currentContextMenu) {
                 this.hideContextMenu();
             }

             // Show only the relevant buttons given the current state.
             var cmd = diagram.commandHandler;
             document.getElementById("Editar").style.display = cmd.canPasteSelection() ? "block" : "none";
             document.getElementById("Incluir Tarefa Irmã").style.display = cmd.canCutSelection() ? "block" : "none";
             document.getElementById("Incluir Tarefa Filha").style.display = cmd.canCopySelection() ? "block" : "none";
             document.getElementById("Excluir").style.display = cmd.canDeleteSelection() ? "block" : "none";
             document.getElementById("color").style.display = obj !== null ? "block" : "none";

             // Now show the whole context menu element
             cxElement.style.display = "block";
             // we don't bother overriding positionContextMenu, we just do it here:
             var mousePt = diagram.lastInput.viewPoint;
             cxElement.style.left = mousePt.x + "px";
             cxElement.style.top = mousePt.y + "px";

             // Remember that there is now a context menu showing
             this.currentContextMenu = contextmenu;
         }

         // This is the corresponding override of ContextMenuTool.hideContextMenu:
         // This does not not need to call the base method.
         cxTool.hideContextMenu = function () {
             if (this.currentContextMenu === null) return;
             cxElement.style.display = "none";
             this.currentContextMenu = null;
         }

         function cxcommand(val) {
             var diagram = myDiagram;
             if (!(diagram.currentTool instanceof go.ContextMenuTool)) return;
             switch (val) {
                 case "Cut": diagram.commandHandler.cutSelection(); break;
                 case "Copy": diagram.commandHandler.copySelection(); break;
                 case "Paste": diagram.commandHandler.pasteSelection(diagram.lastInput.documentPoint); break;
                 case "Delete": diagram.commandHandler.deleteSelection(); break;
                 case "Color": changeColor(diagram); break;
             }
             diagram.currentTool.stopTool();
         }

         // A custom command, for changing the color of the selected node(s).
         function changeColor(diagram) {
             // the object with the context menu, in this case a Node, is accessible as:
             var cmObj = diagram.toolManager.contextMenuTool.currentObject;
             // but this function operates on all selected Nodes, not just the one at the mouse pointer.

             // Always make changes in a transaction, except when initializing the diagram.
             diagram.startTransaction("change color");
             diagram.selection.each(function (node) {
                 if (node instanceof go.Node) {  // ignore any selected Links and simple Parts
                     // Examine and modify the data, not the Node directly.
                     var data = node.data;
                     if (data.color === "red") {
                         // Call setDataProperty to support undo/redo as well as
                         // automatically evaluating any relevant bindings.
                         diagram.model.setDataProperty(data, "color", go.Brush.randomColor());
                     } else {
                         diagram.model.setDataProperty(data, "color", "red");
                     }
                 }
             });
             diagram.commitTransaction("change color");
         }

         function refreshNode(key,coluna,novoValor) {
             var node = myDiagram.findNodeForKey(key);
             var model = myDiagram.model;

             // all model changes should happen in a transaction
             model.startTransaction("change");

             // This is the safe way to change model data
             // GoJS will be notified that the data has changed
             // and can update the node in the Diagram
             // and record the change in the UndoManager

             if(coluna==='NomeTarefa') {
                model.setDataProperty(node.data, "NomeTarefa", novoValor);
             }
             model.commitTransaction("change");
         } // end function refreshNode()

    </script>
</head>
<body>
    <form id="form1" runat="server">

  <div style="display: inline-block;">
    <!-- We make a div to contain both the Diagram div and the context menu (such that they are siblings)
         so that absolute positioning works easily.
         This DIV containing both MUST have a non-static CSS position (we use position: relative)
         so that our context menu's absolute coordinates work correctly. -->
    <div style="position: relative;">
      <div id="myDiagram" style="border: solid 1px black; width:100%; height:750px"></div>
      <div id="myOverview">     </div>
      <div id="contextMenu">
        <ul>
          <li><a href="#" id="cut" onclick="cxcommand(this.textContent)">Cut</a></li>
          <li><a href="#" id="copy" onclick="cxcommand(this.textContent)">Copy</a></li>
          <li><a href="#" id="paste" onclick="cxcommand(this.textContent)">Paste</a></li>
          <li><a href="#" id="delete" onclick="cxcommand(this.textContent)">Delete</a></li>
          <li><a href="#" id="color" onclick="cxcommand('Color')">Color</a></li>
        </ul>
      </div>
    </div>

    <div id="description">
      <p>This demonstrates the implementation of a custom HTML context menu.</p>
      <p>For a light-box style HTML context menu implementation, see the <a href="htmlLightBoxContextMenu.html">LightBox Context Menu</a> sample.</p>
      <p>Right-click or tap-hold on a Node to bring up a context menu.
      If you have a selection copied in the clipboard, you can bring up a context menu anywhere to paste.</p>
    </div>
  </div>




    <div id="sample" style="position: relative;">
        <div id="myDiagramx" style="background-color: white; border: solid 1px black; width: 100%;
            height: 750px">
        </div>
        <div id="myOverviewx">     </div>
        <!-- Styled in a <style> tag at the top of the html page -->
        <p>
            &nbsp;</p>
    </div>
    <dxcp:ASPxCallback ID="cbEAP" ClientInstanceName="cbEAP" runat="server" OnCallback="cbEAP_Callback">
        <ClientSideEvents CallbackComplete="onCallbackComplete" CallbackError="function(s,e){alert('erro');}"
            Init="init2" />
    </dxcp:ASPxCallback>
    <dxcp:ASPxCallback ID="ASPxCallback1" runat="server">
    </dxcp:ASPxCallback>
    <dxcp:ASPxPopupControl ID="pcEditaTarefa" runat="server" 
        ClientInstanceName="pcEditaTarefa" Height="209px" Width="553px" 
        AllowDragging="True" CloseAction="CloseButton" Modal="True" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        HeaderText="Edição tarefa">
        <ContentCollection>
<dxcp:PopupControlContentControl runat="server">
    <table class="dxflInternalEditorTable">
        <tr>
            <td class="style2">
                <dxtv:ASPxLabel ID="lbNomeTarefa" runat="server" Text="Tarefa:" 
                    AssociatedControlID="tbNomeTarefa" ClientInstanceName="lbNomeTarefa">
                </dxtv:ASPxLabel>
            </td>
            <td class="style5">
                <dxtv:ASPxTextBox ID="tbNomeTarefa" runat="server" 
                    ClientInstanceName="tbNomeTarefa" Width="100%">
                </dxtv:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <dxtv:ASPxLabel ID="lbDataInicio" runat="server" Text="Data início:" 
                    AssociatedControlID="deDataInicio" ClientInstanceName="lbDataInicio">
                </dxtv:ASPxLabel>
            </td>
            <td class="style5">
                <dxtv:ASPxDateEdit ID="deDataInicio" runat="server" 
                    ClientInstanceName="deDataInicio" DisplayFormatString="{0:d}">
                </dxtv:ASPxDateEdit>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <dxtv:ASPxLabel ID="lbDataTérmino" runat="server" 
                    AssociatedControlID="deDataTermino" ClientInstanceName="lbDataTérmino" 
                    Text="DataTérmino:">
                </dxtv:ASPxLabel>
            </td>
            <td class="style5">
                <dxtv:ASPxDateEdit ID="deDataTermino" runat="server" 
                    ClientInstanceName="deDataTermino" DisplayFormatString="{0:d}">
                </dxtv:ASPxDateEdit>
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td>
                <dxtv:ASPxButton ID="btnOk" runat="server" ClientInstanceName="btnOk" 
                    Text="Ok" Width="110px" AutoPostBack="False">
                    <ClientSideEvents Click="function(s, e)  {
                        for (var i = 0; i &lt; nodeDataArray.length; i++) {
                            var obj = nodeDataArray[i];
                            if (obj.key == document.getElementById('hfKey').value) {
                                refreshNode(document.getElementById('hfKey').value,'NomeTarefa',tbNomeTarefa.GetText());
                                break;
                            }
                        }
                        
                        pcEditaTarefa.Hide();
                    }" />            
                </dxtv:ASPxButton>

                &nbsp;&nbsp;
                <dxtv:ASPxButton ID="btnCancel" runat="server" ClientInstanceName="btnCancel" 
                    Text="Cancelar" Width="110px" AutoPostBack="False">
                </dxtv:ASPxButton>
            </td>
        </tr>
    </table>
    <input id="hfKey" type="text" style="height:0px; width:0px; visibility:hidden; border-collapse:collapse;" />
            </dxcp:PopupControlContentControl>
</ContentCollection>
    </dxcp:ASPxPopupControl>
    </form>
</body>
</html>
