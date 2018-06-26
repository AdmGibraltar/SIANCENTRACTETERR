using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text.RegularExpressions;

using CapaDatos;
using CapaNegocios;
using LinqToExcel;
using System.Transactions;
using SIANWEB.Utilerias;

namespace SIANWEB.CuentasCorporativas
{
    public partial class Estructura : System.Web.UI.Page
    {

        private List<CatCNac_Estructura> listEstructura
        {
            get
            {
                return (List<CatCNac_Estructura>)Session["listEstructura"];
            }
            set
            {
                Session["listEstructura"] = value;
            }
        }

        private CatCNac_Matriz matriz
        {
            get
            {
                return (CatCNac_Matriz)Session["matriz"];
            }
            set
            {
                Session["matriz"] = value;
            }
        }


        private List<CatCNac_ACYS> listaACYS
        {
            get
            {
                return (List<CatCNac_ACYS>)Session["listaACYS"];
            }
            set
            {
                Session["listaACYS"] = value;
            }
        }

        private List<CatCDI> listaSucursales
        {
            get
            {
                return (List<CatCDI>)Session["listaSucursales"];
            }
            set
            {
                Session["listaSucursales"] = value;
            }
        }

        private Int32 nivelMax
        {
            get
            {
                return (Int32)Session["nivelMax"];
            }
            set
            {
                Session["nivelMax"] = value;
            }
        }




        protected const string UnreadPattern = @"\(\d+\)";
        SIANCENTRAL_CCEntities1 model = new SIANCENTRAL_CCEntities1();
 
   
        protected void RadTreeView1_ContextMenuItemClick(object sender, RadTreeViewContextMenuEventArgs e)
        {
            RadTreeNode clickedNode = e.Node;


            var nodoA = treeEstructura.Nodes[0];
            var hijosPrimerNodo = listEstructura.Where(x => x.Nivel == 1).ToList();

            if (e.MenuItem.Value.StartsWith("[ACYS]"))
            {
                var strValue = e.MenuItem.Value;
                strValue = strValue.Replace("[ACYS]", "");

                int idACYS = Int32.Parse(strValue);


                CN_CatCNac_Estructura cm_EstrACYS = new CN_CatCNac_Estructura(model);
                CatCNac_Estructura estACYS = new CatCNac_Estructura();
                if (clickedNode.Level == nivelMax && nivelMax >= 1)
                {
                    RadMenuItem itemPadre = ((RadMenuItem)(e.MenuItem.Owner));
                    itemPadre.Value = itemPadre.Value.Replace("Asignar", "");

                    estACYS.Id = Int32.Parse(clickedNode.Value);
                    estACYS.Nivel_ACYS = 1;
                    estACYS.id_Acys = idACYS;
                    int id = cm_EstrACYS.CambiarNivel(estACYS);
                    var itemEstr = listEstructura.Where(x => x.Id == estACYS.Id).FirstOrDefault();
                    itemEstr.Nivel_ACYS = estACYS.Nivel_ACYS;
                    itemEstr.id_Acys = idACYS;
                }
                else
                {
                    AsignarACYS(ref clickedNode, clickedNode.Level, idACYS);
                }

                AgregarControles(ref nodoA, hijosPrimerNodo, null);
            }
            else
            switch (e.MenuItem.Value)
            {

                case "Nuevo":
                    RadTreeNode nodoNuevo = new RadTreeNode();
                    nodoNuevo.Selected = true;
                    nodoNuevo.ImageUrl = clickedNode.ImageUrl;
                    clickedNode.Nodes.Add(nodoNuevo);
                    clickedNode.Expanded = true;

                    //set node's value so we can find it in startNodeInEditMode
                    nodoNuevo.Value = "Nuevo - " + Guid.NewGuid().ToString();
                    StartNodeInEditMode(nodoNuevo.Value);

                    AgregarControles(ref nodoA, hijosPrimerNodo, nodoNuevo);

                    break;

                case "Asignar":
                    AsignarACYS(ref clickedNode, clickedNode.Level,1);
                    AgregarControles(ref nodoA, hijosPrimerNodo, null);
                    break;

                case "Asignar1":

                    CN_CatCNac_Estructura cm_Estr1 = new CN_CatCNac_Estructura(model);
                    CatCNac_Estructura est1 = new CatCNac_Estructura();

                    if (clickedNode.Level == nivelMax && nivelMax >= 1)
                    {
                        est1.Id = Int32.Parse(clickedNode.Value);
                        est1.Nivel_ACYS = 1;
                        int id = cm_Estr1.CambiarNivel(est1);
                        var itemEstr = listEstructura.Where(x => x.Id == est1.Id).FirstOrDefault();
                        itemEstr.Nivel_ACYS = 1;
                    }


                    AgregarControles(ref nodoA, hijosPrimerNodo, null);
                    break;

                case "Asignar2":

                    CN_CatCNac_Estructura cm_Estr2 = new CN_CatCNac_Estructura(model);
                    CatCNac_Estructura est2 = new CatCNac_Estructura();

                    if (clickedNode.Level == nivelMax && nivelMax >= 2)
                    {
                        est2.Id = Int32.Parse(clickedNode.Value);
                        est2.Nivel_ACYS = 2;
                        int id = cm_Estr2.CambiarNivel(est2);
                        var itemEstr = listEstructura.Where(x => x.Id == est2.Id).FirstOrDefault();
                        itemEstr.Nivel_ACYS = 2;
                    }

                    AgregarControles(ref nodoA, hijosPrimerNodo, null);

                    break;

                case "Asignar3":

                    CN_CatCNac_Estructura cm_Estr3 = new CN_CatCNac_Estructura(model);
                    CatCNac_Estructura est3 = new CatCNac_Estructura();

                    if (clickedNode.Level == nivelMax && nivelMax >= 3)
                    {
                        est3.Id = Int32.Parse(clickedNode.Value);
                        est3.Nivel_ACYS = 3;
                        int id = cm_Estr3.CambiarNivel(est3);
                        var itemEstr = listEstructura.Where(x => x.Id == est3.Id).FirstOrDefault();
                        itemEstr.Nivel_ACYS = 3;
                    }

                    AgregarControles(ref nodoA, hijosPrimerNodo, null);

                    break;

                case "Asignar4":

                    CN_CatCNac_Estructura cm_Estr4 = new CN_CatCNac_Estructura(model);
                    CatCNac_Estructura est4 = new CatCNac_Estructura();

                    if (clickedNode.Level == nivelMax && nivelMax>=4)
                    {
                        est4.Id = Int32.Parse(clickedNode.Value);
                        est4.Nivel_ACYS = 4;
                        int id = cm_Estr4.CambiarNivel(est4);
                        var itemEstr = listEstructura.Where(x => x.Id == est4.Id).FirstOrDefault();
                        itemEstr.Nivel_ACYS = 4;
                    }

                    AgregarControles(ref nodoA, hijosPrimerNodo, null);

                    break;

                case "Borrar":
                    clickedNode.Remove();

                    CN_CatCNac_Estructura cm_EstrA = new CN_CatCNac_Estructura(model);
                    var estrA = new CatCNac_Estructura();

                    estrA.Id = Int32.Parse(clickedNode.Value);

                    cm_EstrA.Borrar(estrA);
                    listEstructura.Remove(estrA);
                    AgregarControles(ref nodoA, hijosPrimerNodo,null);



                    break;

                default:

                    CN_CatCNac_Estructura cm_EstrD = new CN_CatCNac_Estructura(model);
                    CatCNac_Estructura estD = new CatCNac_Estructura();

                    if (clickedNode.Level == nivelMax && e.MenuItem.Value!="")
                    {
                        estD.Id = Int32.Parse(clickedNode.Value);
                        estD.Sucursal = Int32.Parse(e.MenuItem.Value);
                        estD.NombreSucursal = e.MenuItem.Text;
                        int id = cm_EstrD.CambiarSucursal(estD);
                        
                        var itemEstr = listEstructura.Where(x => x.Id == estD.Id).FirstOrDefault();
                        itemEstr.Sucursal = Int32.Parse(e.MenuItem.Value);
                        itemEstr.NombreSucursal = e.MenuItem.Text;
                    }

                    AgregarControles(ref nodoA, hijosPrimerNodo, null);
                    break;
            }
        }

        private void StartNodeInEditMode(string nodeValue)
        {
            //find the node by its Value and edit it when page loads
            string js = "Sys.Application.add_load(editNode); function editNode(){ ";
            js += "var tree = $find(\"" + this.treeEstructura.ClientID + "\");";
            js += "var node = tree.findNodeByValue('" + nodeValue + "');";
            js += "if (node) node.startEdit();";
            js += "Sys.Application.remove_load(editNode);};";

            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "nodeEdit", js, true);
        }

        protected void RadTreeView1_NodeEdit(object sender, RadTreeNodeEditEventArgs e)
        {
            //string nivelPrefijo = "";
            CN_CatCNac_Estructura cm_Estr = new CN_CatCNac_Estructura(model);
            var estr = new CatCNac_Estructura();
            
            if (e.Node.Value.Contains("Nuevo"))
            {
                estr.Id_Matriz = Int32.Parse(Request.QueryString["Id"]);
                estr.Nivel = e.Node.Level;
                estr.NombreNodo = e.Text;
                estr.NodoPadre = Int32.Parse(e.Node.ParentNode.Value);

                int id = cm_Estr.Alta(estr);
                e.Node.Value = id.ToString();

                listEstructura.Add(estr);
            }
            else
            {
                estr.Id_Matriz = Int32.Parse(Request.QueryString["Id"]);
                estr.Nivel = e.Node.Level;
                estr.NombreNodo = e.Text;
                estr.NodoPadre = Int32.Parse(e.Node.ParentNode.Value);
                estr.Id = Int32.Parse(e.Node.Value);

                int id = cm_Estr.Editar(estr);
                e.Node.Value = id.ToString();
            }

            //if (e.Node.Level == 1) nivelPrefijo = "[" + matriz.Desc_Nivel_1 + "] - ";
            //if (e.Node.Level == 2) nivelPrefijo = "[" + matriz.Desc_Nivel_2 + "] - ";
            //if (e.Node.Level == 3) nivelPrefijo = "[" + matriz.Desc_Nivel_3 + "] - ";
            //if (e.Node.Level == 4) nivelPrefijo = "[" + matriz.Desc_Nivel_4 + "] - ";

            //Label etiq2 = new Label();
            //e.Node.Controls.Add(etiq2);

            e.Node.Text = e.Text;

            var nodoA = treeEstructura.Nodes[0];
            var hijosPrimerNodo = listEstructura.Where(x => x.Nivel == 1).ToList();
            AgregarControles(ref nodoA, hijosPrimerNodo, null);

        }

        //this method is used by Mark All as Read and Empty this folder 
        protected void EmptyFolder(RadTreeNode node, bool removeChildNodes)
        {
            node.Font.Bold = false;
            node.Text = Regex.Replace(node.Text, UnreadPattern, "");

            if (removeChildNodes)
            {
                //Empty this folder is clicked
                for (int i = node.Nodes.Count - 1; i >= 0; i--)
                {
                    node.Nodes.RemoveAt(i);                                                                                                                                   
                }
            }
            else
            {
                //Mark all as read is clicked
                foreach (RadTreeNode child in node.Nodes)
                {
                    EmptyFolder(child, removeChildNodes);
                }
            }
        }    



        protected void Page_Load(object sender, EventArgs e)
        {
            List<CatCNac_Estructura> hijosPrimerNodo = null;
            RadTreeNode nodoOrigen = null;

            int id_ClienteMat = Int32.Parse(Request.QueryString["Id"]);
            string NombreCliente = Request.QueryString["Nombre"];

            nodoOrigen = new RadTreeNode(NombreCliente, "0");
            nodoOrigen.Font.Bold = true;
            nodoOrigen.Font.Size = 11;


            var permisos = new PermisosSesion(this.Page);
            permisos.ValidarSesion();

            if (!Page.IsPostBack)
            {
                //permisos.ValidarPermisos(this.rtb1);


                CN_CatCNac_Estructura cm_Estr = new CN_CatCNac_Estructura(model);
                listEstructura = cm_Estr.ConsultarTodos(id_ClienteMat);

                CN_CatClienteMatriz cm_Matriz = new CN_CatClienteMatriz(model);
                matriz = cm_Matriz.ConsultarMatrizItem(id_ClienteMat);

                CN_CatCNac_ACYS cm_ACYS = new CN_CatCNac_ACYS(model);
                listaACYS = cm_ACYS.ConsultarACYS(id_ClienteMat);

                if (matriz.Nivel_1.Value) nivelMax = 1;
                if (matriz.Nivel_2.Value) nivelMax = 2;
                if (matriz.Nivel_3.Value) nivelMax = 3;
                if (matriz.Nivel_4.Value) nivelMax = 4;

                hijosPrimerNodo = listEstructura.Where(x => x.Nivel == 1).ToList();
                ConstruirEstructura(ref nodoOrigen, hijosPrimerNodo);
                treeEstructura.Nodes.Add(nodoOrigen);


                //Menus
                RadMenuItem menuNuevo = new RadMenuItem("Nuevo...");
                menuNuevo.Value = "Nuevo";
                RadMenuItem menuBorrar = new RadMenuItem("Borrar");
                menuBorrar.Value = "Borrar";
                RadMenuItem menuAsignar = new RadMenuItem("Asignar");
                menuAsignar.Value = "Asignar";
                //RadMenuItem menuAsignar1 = new RadMenuItem("Asignar1");


                RadMenuItem menuNuevo_A = new RadMenuItem("Nuevo...");
                menuNuevo_A.Value = "Nuevo";
                RadMenuItem menuBorrar_A = new RadMenuItem("Borrar");
                menuBorrar_A.Value = "Borrar";
                RadMenuItem menuAsignar_A = new RadMenuItem("Asignar");
                menuAsignar_A.Value = "Asignar";
                //RadMenuItem menuAsignar1_A = new RadMenuItem("Asignar1");
                //menuAsignar1_A.Value = "Asignar1";
                //RadMenuItem menuAsignar2_A = new RadMenuItem("Asignar2");
                //menuAsignar2_A.Value = "Asignar2";


                RadMenuItem menuNuevo_B = new RadMenuItem("Nuevo...");
                menuNuevo_B.Value = "Nuevo";
                RadMenuItem menuBorrar_B = new RadMenuItem("Borrar");
                menuBorrar_B.Value = "Borrar";
                RadMenuItem menuAsignar_B = new RadMenuItem("Asignar");
                menuAsignar_B.Value = "Asignar";
                //RadMenuItem menuAsignar1_B = new RadMenuItem("Asignar1");
                //menuAsignar1_B.Value = "Asignar1";
                //RadMenuItem menuAsignar2_B = new RadMenuItem("Asignar2");
                //menuAsignar2_B.Value = "Asignar2";
                //RadMenuItem menuAsignar3_B = new RadMenuItem("Asignar3");
                //menuAsignar3_B.Value = "Asignar3";

                RadMenuItem menuNuevo_C = new RadMenuItem("Nuevo...");
                menuNuevo_C.Value = "Nuevo";
                RadMenuItem menuBorrar_C = new RadMenuItem("Borrar");
                menuBorrar_C.Value = "Borrar";
                RadMenuItem menuAsignar_C = new RadMenuItem("Asignar");
                menuAsignar_C.Value = "Asignar";
                RadMenuItem menuAsignar1_C = new RadMenuItem("Asignar1");
                menuAsignar1_C.Value = "Asignar1";
                RadMenuItem menuAsignar2_C = new RadMenuItem("Asignar2");
                menuAsignar2_C.Value = "Asignar2";
                RadMenuItem menuAsignar3_C = new RadMenuItem("Asignar3");
                menuAsignar3_C.Value = "Asignar3";
                RadMenuItem menuAsignar4_C = new RadMenuItem("Asignar4");
                menuAsignar4_C.Value = "Asignar4";


                RadTreeViewContextMenu menuNodos1 = new RadTreeViewContextMenu();
                menuNodos1.ID = "menuNodos1";
                menuNodos1.Items.Add(menuNuevo);
                menuNodos1.Items.Add(menuBorrar);
                menuNodos1.Items.Add(menuAsignar);
    ;

                RadTreeViewContextMenu menuNodos2 = new RadTreeViewContextMenu();
                menuNodos2.ID = "menuNodos2";
                menuNodos2.Items.Add(menuNuevo_A);
                menuNodos2.Items.Add(menuBorrar_A);
                menuNodos2.Items.Add(menuAsignar_A);


                RadTreeViewContextMenu menuNodos3 = new RadTreeViewContextMenu();
                menuNodos3.ID = "menuNodos3";
                menuNodos3.Items.Add(menuNuevo_B);
                menuNodos3.Items.Add(menuBorrar_B);
                menuNodos3.Items.Add(menuAsignar_B);


                RadTreeViewContextMenu menuNodos4 = new RadTreeViewContextMenu();
                menuNodos4.ID = "menuNodos4";
                menuNodos4.Items.Add(menuNuevo_C);
                menuNodos4.Items.Add(menuBorrar_C);
                menuNodos4.Items.Add(menuAsignar_C);
                menuNodos4.Items.Add(menuAsignar1_C);
                menuNodos4.Items.Add(menuAsignar2_C);
                menuNodos4.Items.Add(menuAsignar3_C);
                menuNodos4.Items.Add(menuAsignar4_C);


                treeEstructura.ContextMenus.Add(menuNodos1);
                treeEstructura.ContextMenus.Add(menuNodos2);
                treeEstructura.ContextMenus.Add(menuNodos3);
                treeEstructura.ContextMenus.Add(menuNodos4);


                AgregarControles(ref nodoOrigen, hijosPrimerNodo,null);



                    // ConsultaTipos()
                    CN_CatClienteMatriz cnMat = new CN_CatClienteMatriz(model);
                    var sucursales = cnMat.ConsultaTipos();

                    listaSucursales = sucursales;

                    var menuSuc = new RadMenuItem("Sucursales");
                    foreach (CatCDI suc in sucursales)
                    {
                        RadMenuItem itemAgregar = new RadMenuItem(suc.Id_Cd.ToString() + "-" + suc.Cd_Nombre);
                        itemAgregar.Value = suc.Id_Cd.ToString();
                        menuSuc.Items.Add(itemAgregar);
                    }


                    var menuA = treeEstructura.ContextMenus[3];
                    menuA.Items.Add(menuSuc);

                    var menuAsig1 = menuA.Items[3];
                    var acys1 = listaACYS.Where(x => x.NivelAcys == 1).ToList();
                    foreach (CatCNac_ACYS ac in acys1)
                    {
                        RadMenuItem itemAgregar = new RadMenuItem(ac.Nombre);
                        itemAgregar.Value = "[ACYS]" + ac.Id.ToString();
                        menuAsig1.Items.Add(itemAgregar);
                    }

                    var menuAsig2 = menuA.Items[4];
                    var acys2 = listaACYS.Where(x => x.NivelAcys == 2).ToList();
                    foreach (CatCNac_ACYS ac in acys2)
                    {
                        RadMenuItem itemAgregar = new RadMenuItem(ac.Nombre);
                        itemAgregar.Value = "[ACYS]" + ac.Id.ToString();
                        menuAsig2.Items.Add(itemAgregar);
                    }

                    var menuAsig3 = menuA.Items[5];
                    var acys3 = listaACYS.Where(x => x.NivelAcys == 3).ToList();
                    foreach (CatCNac_ACYS ac in acys3)
                    {
                        RadMenuItem itemAgregar = new RadMenuItem(ac.Nombre);
                        itemAgregar.Value = "[ACYS]" + ac.Id.ToString();
                        menuAsig3.Items.Add(itemAgregar);
                    }

                    var menuAsig4 = menuA.Items[6];
                    var acys4 = listaACYS.Where(x => x.NivelAcys == 4).ToList();
                    foreach (CatCNac_ACYS ac in acys4)
                    {
                        RadMenuItem itemAgregar = new RadMenuItem(ac.Nombre);
                        itemAgregar.Value = "[ACYS]" + ac.Id.ToString();
                        menuAsig4.Items.Add(itemAgregar);
                    }


                        var menuB = treeEstructura.ContextMenus[0];

                        foreach (CatCNac_ACYS ac in acys1)
                        {
                            RadMenuItem itemAgregar = new RadMenuItem(ac.Nombre);
                            itemAgregar.Value = "[ACYS]" + ac.Id.ToString();
                            menuB.Items[2].Items.Add(itemAgregar);
                        }

                        var menuC = treeEstructura.ContextMenus[1];

                        foreach (CatCNac_ACYS ac in acys2)
                        {
                            RadMenuItem itemAgregar = new RadMenuItem(ac.Nombre);
                            itemAgregar.Value = "[ACYS]" + ac.Id.ToString();
                            menuC.Items[2].Items.Add(itemAgregar);
                        }

                        var menuD = treeEstructura.ContextMenus[2];

                        foreach (CatCNac_ACYS ac in acys3)
                        {
                            RadMenuItem itemAgregar = new RadMenuItem(ac.Nombre);
                            itemAgregar.Value = "[ACYS]" + ac.Id.ToString();
                            menuD.Items[2].Items.Add(itemAgregar);
                        }
              
            }


           
            //hijosPrimerNodo = listEstructura.Where(x => x.Nivel == 1).ToList();

           // treeEstructura.ExpandAllNodes();
        }



        protected void ConstruirEstructura(ref RadTreeNode nodoArbol, List<CatCNac_Estructura> nodosHijos)
        {

            foreach (CatCNac_Estructura nodo in nodosHijos)
            {

                if (nodosHijos.Count > 0)
                {
                    var nodoAgregar = new RadTreeNode(nodo.NombreNodo);
                    nodoAgregar.Value = nodo.Id.ToString();
                    nodoArbol.Nodes.Add(nodoAgregar);
                    var hijosNodoActual = listEstructura.Where(x => x.NodoPadre == nodo.Id).ToList();
                    ConstruirEstructura(ref nodoAgregar, hijosNodoActual);
                }

            }


        }


        protected void AgregarControles(ref RadTreeNode nodoArbol, List<CatCNac_Estructura> nodosHijos, RadTreeNode nodoEditando)
        {
            // Crea menus
            foreach(RadTreeNode nodo in nodoArbol.Nodes)
            {
                var nodoA = nodo;
                var nivel = "";


                if (nodoA.Level == 1)
                {
                    nivel = "[" + matriz.Desc_Nivel_1 + "]";
                    nodoA.Font.Size = 10;
                    nodoA.ContextMenuID = "menuNodos1";

                }

                if (nodoA.Level == 2)
                {
                    nivel = "[" + matriz.Desc_Nivel_2 + "]";
                    nodoA.Font.Size = 10;
                    nodoA.ContextMenuID = "menuNodos2";


                    var menuA = treeEstructura.ContextMenus[1];
                    var acys2 = listaACYS.Where(x => x.NivelAcys == 2).ToList();
                }


                if (nodoA.Level == 3)
                {
                    nivel = "[" + matriz.Desc_Nivel_3 + "]";
                    nodoA.Font.Size = 10;
                    nodoA.ContextMenuID = "menuNodos3";

                    var menuA = treeEstructura.ContextMenus[2];
                    var acys3 = listaACYS.Where(x => x.NivelAcys == 3).ToList();


                }

                if (nodoA.Level == 4)
                {
                    nivel = "[" + matriz.Desc_Nivel_4 + "]";
                    nodoA.Font.Size = 9;
                    nodoA.ContextMenuID = "menuNodos4";
                }

                Label etiq2 = new Label();
                etiq2.Text = nivel + " - ";
                etiq2.ForeColor = System.Drawing.Color.Blue;
                etiq2.Font.Bold = true;

                Label etiq = new Label();
                etiq.Text = nodoA.Text;

                if (nodoEditando==null || nodoA.Value != nodoEditando.Value)
                {
                    nodoA.Controls.Add(etiq2);
                    nodoA.Controls.Add(etiq);


                    var res = this.listEstructura.Where(x => x.Id == Int32.Parse(nodoA.Value)).FirstOrDefault();
                    Label etiq3 = new Label();
                    Label etiq4 = new Label();
                    Label etiq5 = new Label();

                    if (res != null && res.Nivel_ACYS!=null)
                    {
                        var acys= listaACYS.Where(x => x.Id == res.id_Acys).FirstOrDefault();


                        etiq3.Text = " - ACYS Considerar: Nivel: " + res.Nivel_ACYS.ToString();
                        if (acys != null) etiq3.Text += " - " + acys.Nombre;

                        etiq3.ForeColor = System.Drawing.Color.Green;
                        etiq3.Font.Bold = true;
                        nodoA.Controls.Add(etiq3);
                    }

                    if (res != null && res.Sucursal != null)
                    {
                        etiq4.Text = " - Sucursal: " + res.NombreSucursal;
                        etiq4.ForeColor = System.Drawing.Color.Orange;
                        etiq4.Font.Bold = true;
                        nodoA.Controls.Add(etiq4);
                    }



                    if (res != null && res.id_Cte != null)
                    {
                        etiq5.Text = " - Cliente: " + res.id_Cte.ToString() + " " + res.NombreCliente;
                        etiq5.ForeColor = System.Drawing.Color.DarkRed;
                        etiq5.Font.Bold = true;
                        nodoA.Controls.Add(etiq5);
                    }

                }
                AgregarControles(ref nodoA, nodosHijos, nodoEditando);

            }

        }



        protected void AsignarACYS(ref RadTreeNode nodoArbol, int nivel, int idAcys)
        {
            CN_CatCNac_Estructura cm_Estr = new CN_CatCNac_Estructura(model);
            CatCNac_Estructura estr = new CatCNac_Estructura();





            foreach (RadTreeNode nodo in nodoArbol.Nodes)
            {

                    var nodoA = nodo;
                    if (nodo.Level ==nivelMax)
                    {
                        estr.Id = Int32.Parse(nodo.Value);
                        estr.Nivel_ACYS = nivel;
                        estr.id_Acys = idAcys;
                        int id = cm_Estr.CambiarNivel(estr);
                        var itemEstr = listEstructura.Where(x => x.Id == estr.Id).FirstOrDefault();
                        itemEstr.Nivel_ACYS = nivel;
                        itemEstr.id_Acys = idAcys;
                    }
                    AsignarACYS(ref nodoA, nivel, idAcys);


                
            }

        }


        protected void Page_Unload(object sender, EventArgs e)
        {

        }


        protected void btnCargaExcel_Click(object sender, EventArgs e)
        {

            HttpPostedFile filePosted = Request.Files[0];

            string pathArchivo = "c:\\KeyWeb_Log\\excelTempCC.xlsx";
            filePosted.SaveAs(pathArchivo);

            var excelFile = new ExcelQueryFactory(pathArchivo);
            var miHoja = excelFile.Worksheet(0).Select(x => x).ToList() ;
            CN_CatCNac_Estructura cm_Estr = new CN_CatCNac_Estructura(model);



            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    cm_Estr.Borrar(Int32.Parse(Request.QueryString["Id"]));
                    listEstructura.Clear();


                    int padre1 = 0;
                    int padre2 = 0;
                    int padre3 = 0;
                    int padre4 = 0;


                    

                    foreach (var item in miHoja)
                    {

                        CatCNac_Estructura est = new CatCNac_Estructura();
                        est.Id_Matriz = Int32.Parse(Request.QueryString["Id"]);

                        //Nivel 1


                        if (item["Nivel ACYS"] == null) throw new Exception("Falta la columna Nivel ACYS");
                        if (item["ACYS"] == null) throw new Exception("Falta la columna ACYS");
                        if (item["Sucursal"] == null) throw new Exception("Falta la columna Sucursal");






                        if (nivelMax >= 1)
                        {

                            est.NombreNodo = item[0].Value.ToString();
                            if (!this.listEstructura.Exists(x => x.NombreNodo == est.NombreNodo && x.Id_Matriz == est.Id_Matriz && x.Nivel == 1))
                            {

                                est.Nivel = 1;
                                est.NodoPadre = 0;

                                if (nivelMax == 1)
                                {
                                    est.Nivel_ACYS = item["Nivel ACYS"].Cast<int>();

                                    var acysC=listaACYS.Where(x => x.Nombre == item["ACYS"]).FirstOrDefault();

                                    if (acysC == null) throw new Exception("El acys no existe: " + item["ACYS"]);

                                    int idAcys = acysC.Id;
                                    est.id_Acys = idAcys;

                                    est.Sucursal = item["Sucursal"].Cast<int>();
                                    var suc=listaSucursales.Where(x => x.Id_Cd == est.Sucursal).FirstOrDefault();

                                    if (suc == null) throw new Exception("La Sucursal no existe: " + item["Sucursal"]);

                                 
                                    est.NombreSucursal = item["Sucursal"] + " - " + suc.Cd_Nombre;

                                }


                                int resId = cm_Estr.Alta(est);
                                est.Id = resId;
                                padre1 = resId;
                                listEstructura.Add(est);
                            }
                        }



                        if (nivelMax >= 2)
                        {

                            //Nivel 2
                            est = new CatCNac_Estructura();
                            est.NombreNodo = item[1].Value.ToString();
                            est.Id_Matriz = Int32.Parse(Request.QueryString["Id"]);
                            if (!this.listEstructura.Exists(x => x.NombreNodo == est.NombreNodo && x.Id_Matriz == est.Id_Matriz && x.Nivel == 2 && x.NodoPadre == padre1))
                            {

                                est.Id_Matriz = Int32.Parse(Request.QueryString["Id"]);
                                est.Nivel = 2;
                                est.NodoPadre = padre1;

                                if (nivelMax == 2)
                                {
                                    est.Nivel_ACYS = item["Nivel ACYS"].Cast<int>();

                                    var acysC = listaACYS.Where(x => x.Nombre == item["ACYS"]).FirstOrDefault();

                                    if (acysC == null) throw new Exception("El acys no existe: " + item["ACYS"]);

                                    int idAcys = acysC.Id;
                                    est.id_Acys = idAcys;

                                    est.Sucursal = item["Sucursal"].Cast<int>();
                                    var suc = listaSucursales.Where(x => x.Id_Cd == est.Sucursal).FirstOrDefault();

                                    if (suc == null) throw new Exception("La Sucursal no existe: " + item["Sucursal"]);
                                    
                        
                                        est.NombreSucursal = item["Sucursal"] + " - " + suc.Cd_Nombre;
                                    
                                }

                                int resId = cm_Estr.Alta(est);
                                est.Id = resId;
                                padre2 = resId;
                                listEstructura.Add(est);
                            }

                        }

                        if (nivelMax >= 3)
                        {

                            //Nivel 3
                            est = new CatCNac_Estructura();
                            est.NombreNodo = item[2].Value.ToString();
                            est.Id_Matriz = Int32.Parse(Request.QueryString["Id"]);
                            if (!this.listEstructura.Exists(x => x.NombreNodo == est.NombreNodo && x.Id_Matriz == est.Id_Matriz && x.Nivel == 3 && x.NodoPadre == padre2))
                            {

                                est.Id_Matriz = Int32.Parse(Request.QueryString["Id"]);
                                est.Nivel = 3;
                                est.NodoPadre = padre2;


                                if (nivelMax == 3)
                                {
                                    est.Nivel_ACYS = item["Nivel ACYS"].Cast<int>();

                                    var acysC = listaACYS.Where(x => x.Nombre == item["ACYS"]).FirstOrDefault();

                                    if (acysC == null) throw new Exception("El acys no existe: " + item["ACYS"]);

                                    int idAcys = acysC.Id;
                                    est.id_Acys = idAcys;


                                    est.Sucursal = item["Sucursal"].Cast<int>();
                                    var suc = listaSucursales.Where(x => x.Id_Cd == est.Sucursal).FirstOrDefault();

                                    if (suc == null) throw new Exception("La Sucursal no existe: " + item["Sucursal"]);

                            
                                    est.NombreSucursal = item["Sucursal"] + " - " + suc.Cd_Nombre;

                                }

                                int resId = cm_Estr.Alta(est);
                                est.Id = resId;
                                padre3 = resId;
                                listEstructura.Add(est);
                            }
                        }


                        if (nivelMax >= 4)
                        {
                            //Nivel 4
                            est = new CatCNac_Estructura();
                            est.NombreNodo = item[3].Value.ToString();
                            est.Id_Matriz = Int32.Parse(Request.QueryString["Id"]);
                            if (!this.listEstructura.Exists(x => x.NombreNodo == est.NombreNodo && x.Id_Matriz == est.Id_Matriz && x.Nivel == 4 && x.NodoPadre == padre3))
                            {

                                est.Id_Matriz = Int32.Parse(Request.QueryString["Id"]);
                                est.Nivel = 4;
                                est.NodoPadre = padre3;

                                est.Nivel_ACYS = item["Nivel ACYS"].Cast<int>();

                                var acysC = listaACYS.Where(x => x.Nombre == item["ACYS"]).FirstOrDefault();

                                if (acysC == null) throw new Exception("El acys no existe: " + item["ACYS"]);

                                int idAcys = acysC.Id;
                                est.id_Acys = idAcys;

                                  est.Sucursal = item["Sucursal"].Cast<int>();
                                var suc = listaSucursales.Where(x => x.Id_Cd == est.Sucursal).FirstOrDefault();

                                if (suc == null) throw new Exception("La Sucursal no existe: " + item["Sucursal"]);

                              
                                est.NombreSucursal = item["Sucursal"] + " - " + suc.Cd_Nombre;

                                int resId = cm_Estr.Alta(est);  
                                est.Id = resId;
                                padre4 = resId;
                                listEstructura.Add(est);

                            }
                        }

                    }

                    model.SaveChanges();
                    scope.Complete();
                   

                }
                catch (Exception ex)
                {
                    scope.Dispose();

                    listEstructura.Clear();
                    listEstructura = cm_Estr.ConsultarTodos(Int32.Parse(Request.QueryString["Id"]));
                    RAM1.ResponseScripts.Add("CloseAlert('"+ ex.Message+ "')");
                    
                }
                finally
                {
                    var nodoA = treeEstructura.Nodes[0];
                    var hijosPrimerNodo = listEstructura.Where(x => x.Nivel == 1).ToList();
                    nodoA.Nodes.Clear();
                    ConstruirEstructura(ref nodoA, hijosPrimerNodo);
                    AgregarControles(ref nodoA, hijosPrimerNodo, null);
                    treeEstructura.ExpandAllNodes();
                }
            }

            
        }

    }
}