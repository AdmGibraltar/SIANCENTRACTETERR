<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Estructura.aspx.cs" Inherits="SIANWEB.CuentasCorporativas.Estructura" 
  MasterPageFile="~/MasterPage/MasterPage02.master" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">


    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>

     <telerik:radajaxmanager id="RAM1" runat="server" enablepageheadupdate="False">
     </telerik:radajaxmanager>

      <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
      </telerik:RadWindowManager>

    <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" >
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="Guardar" />
        </Items>
    </telerik:radtoolbar>


 <h4> Estructura del cliente </h4>
<p></p>

<p style="text-align:right">
<input type="file" runat="server" id="FileExcel"/>&nbsp;<asp:Button 
        ID="btnCargaExcel" runat="server" Text="Cargar excel..." 
        onclick="btnCargaExcel_Click" />
</p>

<div style="overflow:auto;height:80%">
  <telerik:RadTreeView RenderMode="Lightweight" runat="Server" ID="treeEstructura" EnableDragAndDrop="true" 
                     EnableDragAndDropBetweenNodes="false" Skin="Vista" OnContextMenuItemClick="RadTreeView1_ContextMenuItemClick"
                      OnNodeEdit="RadTreeView1_NodeEdit"
                       AllowNodeEditing="true" ExpandAnimation-Type="Linear">
                
                 <ContextMenus>
 



                </ContextMenus>

                    <Nodes>

        
                    </Nodes>
        
   </telerik:RadTreeView>
</div>

   
      <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            (function () {

                window.onClientContextMenuShowing = function (sender, args) {
                    var treeNode = args.get_node();
                    treeNode.set_selected(true);
                    //enable/disable menu items
                    setMenuItemsState(args.get_menu().get_items(), treeNode);
                };

                window.onClientContextMenuItemClicking = function (sender, args) {
                    var menuItem = args.get_menuItem();
                    var treeNode = args.get_node();
                    menuItem.get_menu().hide();

                    switch (menuItem.get_value()) {
                        case "Copy":
                            break;
                        case "Rename":
                            treeNode.startEdit();
                            break;
                        case "NewFolder":
                            break;
                        case "MarkAsRead":
                            break;
                        case "Delete":
                            var result = confirm("Are you sure you want to delete the item: " + treeNode.get_text());
                            args.set_cancel(!result);
                            break;
                    }
                };

                //this method disables the appropriate context menu items
                function setMenuItemsState(menuItems, treeNode) {
                    for (var i = 0; i < menuItems.get_count(); i++) {
                        var menuItem = menuItems.getItem(i);
                        switch (menuItem.get_value()) {
                            case "Copy":
                                formatMenuItem(menuItem, treeNode, 'Copy "{0}"');
                                break;
                            case "Rename":
                                formatMenuItem(menuItem, treeNode, 'Rename "{0}"');
                                break;
                            case "Delete":
                                formatMenuItem(menuItem, treeNode, 'Delete "{0}"');
                                break;
                            case "NewFolder":
                                if (treeNode.get_parent() == treeNode.get_treeView()) {
                                    menuItem.set_enabled(false);
                                }
                                else {
                                    menuItem.set_enabled(true);
                                }
                                break;
                            case "MarkAsRead":
                                var enabled = hasNodeMails(treeNode);
                                menuItem.set_enabled(enabled != null);
                                break;
                        }
                    }
                }

                //formats the Text of the menu item
                function formatMenuItem(menuItem, treeNode, formatString) {
                    var nodeValue = treeNode.get_value();
                    if (nodeValue && nodeValue.indexOf("_Private_") == 0) {
                        menuItem.set_enabled(false);
                    }
                    else {
                        menuItem.set_enabled(true);
                    }
                    var newText = String.format(formatString, extractTitleWithoutMails(treeNode));
                    menuItem.set_text(newText);
                }

                //checks if the text contains (digit)
                function hasNodeMails(treeNode) {
                    return treeNode.get_text().match(/\([\d]+\)/ig);
                }

                //removes the brackets with the numbers,e.g. Inbox (30)
                function extractTitleWithoutMails(treeNode) {
                    return treeNode.get_text().replace(/\s*\([\d]+\)\s*/ig, "");
                }
            } ());

            function CloseWindow() {
                GetRadWindow().Close();
            }

            function CloseAlert(mensaje) {
                //                var cerrarWindow = radalert(mensaje, 330, 150);
                //                cerrarWindow.add_close(
                //                    function () {

                alert(mensaje);
                //CloseWindow();
                //                    });
            }


            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }


        </script>
     </telerik:radcodeblock>

  

</asp:Content>
