using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using System.Data;
using CapaNegocios;
using System.Collections;

namespace SIANWEB
{
    public partial class CapCompensacionVariable : System.Web.UI.Page
    {

        DataTable dtValues;
        DataTable dtValuesVar;

        private int id_Sistema = 0;

        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }


        //inicio de código de el fgrid del detalle

            private static readonly Random random = new Random();

            static string[] contactNames = new string[] { "Antonio Moreno", "Elizabeth Lincoln"};
            static double[] importes = { 2232.5, 3122.4};
            static int[] dias = { 2, 3,4,40,50,23,12 };
            protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
            {
                RadGrid1.DataSource = dataSource;
            }

            public List<Customer> dataSource
            {
                get
                {
                    object obj = Application["VirtualizationDataSource"];
                    if (obj == null)
                    {
                        List<Customer> customers = new List<Customer>();
                        for (int i = 0; i < 1; i++)
                        {
                            int titlesIndex = random.Next() % 2;
                            int companyIndex = random.Next() % 2;
                            int contactIndex = random.Next() % 2;
                            int ratingIndex = random.Next() % 2;
                            int diasIndex = random.Next() % 7;
                            Customer customer = new Customer();
                            customer.Pag_Referencia = i + 1;
                            customer.Id_Cte = i + 1;
                            customer.Id_Ter = i + 1;
                            customer.Id_RIK = i + 1;
                            customer.Dias = dias[diasIndex];

                            customer.Vencimiento = DateTime.Now;
                            customer.FechaPago = DateTime.Now;
                            customer.Importe = importes[ratingIndex];
                            customer.UP = importes[ratingIndex]-234;
                            customer.AjCobranza = importes[ratingIndex]-543;
                            customer.Mult_Porc = importes[ratingIndex] - 145;

                           
                            customers.Add(customer);
                        }
                        Application["VirtualizationDataSource"] = customers;
                    }
                    return (List<Customer>)Application["VirtualizationDataSource"];
                }
                set
                {
                    Application["VirtualizationDataSource"] = value;
                }
            }

            [Serializable]
            public class Customer
            {
                public int Pag_Referencia { get; set; }
                public int Id_Cte { get; set; }
                public int Id_Ter { get; set; }
                public int Id_RIK { get; set; }
                public int Dias { get; set; }
                public DateTime FechaPago { get; set; }
                public DateTime Vencimiento { get; set; }
                public double Importe { get; set; }
                public double UP { get; set; }
                public double AjCobranza { get; set; }
                public double Mult_Porc { get; set; }
                
            }
    //fin de código de el fgrid del detalle

        protected void Page_Load(object sender, EventArgs e)
        {

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            id_Sistema = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);

            if (Sesion == null)
            {
                //string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                //Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                //RAM1.ResponseScripts.Add("CloseWindow();");
            }
            else
            {


                if (!IsPostBack)
                {

                    if (id_Sistema == 0)
                    {
                        this.hiddenId.Value = string.Empty;
                        id_Sistema = 0;
                        Nuevo();
                    }
                    else
                    {
                        this.hiddenId.Value = id_Sistema.ToString();
                        Inicializar();
                    }

                     
                    
                    //ValidarPermisos();
               

                }

                grVariables.Visible = true;
                txtFormula.Visible = false;
                rgGrid.Visible = false;


                //////grVariables.MasterTableView.EditMode = (GridEditMode)Enum.Parse(typeof(GridEditMode), "PopUp");
                //////grVariables.Rebind();
       


               
                 
            
                //rgGrid.Visible = false;
                //txtArea.Visible = false;


                ////RadTreeView1.LoadContentFile("treeView.xml");
                ////RadTreeView1.ExpandAllNodes();
                ////RadTreeView2.LoadContentFile("treeView.xml");
                ////RadTreeView2.ExpandAllNodes();
              
            }
            //JFCV 24oct2016 que pueda teclearse el numero de proveedor punto 4
            //txtProveedor.TextChanged += new EventHandler(RadInput_TextChanged);


        }


        protected void btnAgregar_Click(object sender, EventArgs arg)
        {
            try
            {
                string svariable = "";
                string svariableformula = "";
                if (HD_FormulaPaso.Value == "0")
                {
                   txtFormula.Text = "";
                }


                if (txtConcepto.Text.Trim() == "")
                {
                    Alerta("Por favor teclee el concepto.");
                    return;
                }
                if (txtDescripcion.Text.Trim() == "")
                {
                    Alerta("Por favor teclee la descripción.");
                    return;
                }
                if (cmbVariables.SelectedIndex == 17 && txtValor.Text.Trim() == "" )
                {
                    Alerta("Por favor si elige la opción de constante de usuario debe teclear un valor para la constante.");
                    return;
                }

                if (cmbVariables.SelectedIndex != 17 && txtValor.Text.Trim() != "")
                {
                    Alerta("Si elige una opción diferente a constante de usuario no debe teclear un valor para la constante.");
                    return;
                }

             

                int caseSwitch = Convert.ToInt32(cmbVariables.SelectedIndex);
                switch (caseSwitch)
                {
                    case 0:
                        svariable = "";
                        break;
                    case 1:
                        svariable = "IVC";
                        break;
                    case 2:
                        svariable = "UP";
                        break;
                    case 3:
                        svariable = "ASP";
                        break;
                         case 4:
                        svariable = "GTS";
                        break;
                         case 5:
                        svariable = "AAER";
                        break;
                    case 6:
                        svariable = "MO";
                        break;
                    case 7:
                        svariable = "AEA";
                        break;
  
                    case 8:
                        svariable = "FC";
                        break;
                    case 9:
                        svariable = "UBC";
                        break;
                    case 10:
                        svariable = "FPPP";
                        break;
                    case 11:
                        svariable = "MVI";
                        break;
                    case 12:
                        svariable = "CND";
                        break;
                    case 13:
                        svariable = "SF";
                        break;
                    case 14:
                        svariable = "TSF";
                        break;
                    case 15:
                        svariable = "GA";
                        break;
                    case 16:
                        svariable = "CB";
                        break;
                    case 17:
                        //svariable = "MVI";
                        svariable = txtValor.Text;
                        break;
                    default:
                        svariable = cmbVariables.SelectedItem.Text;
                        //Si eligío una formula del combo entonces tengo que buscar en el grid de formulas la formula de la que eligio y ponerla aqui
                        string formula = "";
                        dtValuesVar = (DataTable)Session["TableVar"];
                        foreach (DataRow row in dtValuesVar.Rows)
                        {
                            if (Convert.ToString(row["sVariable_Local"]) == svariable)
                            {
                                formula = row["sVariable_Formula"].ToString();
                            }
                        }
                        svariableformula = cmbOperador.SelectedItem.Text + formula;
                        break;
                }

                

                if (HD_FormulaPaso.Value == "0")
                {
                    txtArea.Text = txtArea.Text + txtConcepto.Text + " = " + cmbOperador.SelectedItem.Text + svariable  ;
                    HD_FormulaPaso.Value = txtConcepto.Text + " = " + cmbOperador.SelectedItem.Text + svariable ;
               }
                else
                {
                    txtArea.Text = txtArea.Text + cmbOperador.SelectedItem.Text + svariable  ;
                    HD_FormulaPaso.Value = HD_FormulaPaso.Value + cmbOperador.SelectedItem.Text + svariable ;
                }

                if (svariableformula == "")
                    svariableformula = cmbOperador.SelectedItem.Text + svariable;

                 txtFormula.Text = txtFormula.Text + svariableformula;

                //if (textBox1.Lines.Length > 0)
                //    textBox2.Text = textBox1.Lines[0];
                //TextBox[] text = new TextBox[] { textBox2, textBox3, textBox4 };
                //if (textBox.Lines.Length >= 3)
                //{
                //    for (int x = 0; x < 3; x++)
                //        text[x] = textBox1.Lines[x];
                //}


                //if ((cmbCtaGastos.SelectedValue != "") && (TxtFechaRequiere.SelectedDate != null))
                //{
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                GastoViajeComprobante comprobante = new GastoViajeComprobante();

                //JFCV primero vamos aver si es con comprobante o sin comprobante 


                comprobante.Id_Emp = Sesion.Id_Emp;
                comprobante.Id_Cd = Sesion.Id_Cd_Ver;
                //comprobante.Id_GV = Int32.Parse(Request.QueryString["Id"] == null ? "-1" : Request.QueryString["Id"]);
                //comprobante.GVComprobante_Fecha = TxtFechaRequiere.SelectedDate;
                //comprobante.GVComprobante_ConComprobante = true;
                //comprobante.Id_GVComprobanteTipo = 1;
                //comprobante.GVComprobante_Observaciones = TxtObservaciones.Text;

                //comprobante.GVComprobante_GV_Cuenta = TxtCuenta.Text.Trim();
                //comprobante.GVComprobante_GV_Cc = TxtCc.Text.Trim();
                //comprobante.GVComprobante_GV_CuentaPago = TxtCuentaPago.Text.Trim();

                //comprobante.GVComprobante_GV_Numero = TxtNumero.Text.Trim();
                //comprobante.GVComprobante_GV_SubCuenta = TxtSubCuenta.Text.Trim();
                //comprobante.GVComprobante_GV_SubSubCuenta = TxtSubSubCuenta.Text.Trim();



                //////if (cmbFactura.Visible)
                //////{
                int partidarow = 0;

                dtValues = (DataTable)Session["Table"];
                foreach (DataRow row in dtValues.Rows)
                {
                    if (Convert.ToInt32(row["Id_ConceptoVariable"].ToString()) > partidarow)
                    {
                        partidarow = Convert.ToInt32(row["Id_ConceptoVariable"].ToString());
                    }
                }

                partidarow += 1;



                //// Valido si la factura ya existe en el grid y si es asi no la agrego
                //// para validar comparo folio serie e importe 
                //// insertar si o no 
                //bool facturaexiste = false;

                //foreach (DataRow row in dtValues.Rows)
                //{
                //    if (Convert.ToString(row["PagElec_Serie"].ToString()) == (string)DT.Rows[0]["serie"])
                //    {
                //        if (Convert.ToString(row["PagElec_Folio"].ToString()) == (string)DT.Rows[0]["folio"])
                //        {
                //            if (Convert.ToDecimal(row["GVComprobante_Importe"].ToString()) == Convert.ToDecimal(DT.Rows[0]["importe"]))
                //            {
                //                facturaexiste = true;
                //            }
                //        }
                //    }
                //}


                //comprobante.GVComprobante_Serie = (string)DT.Rows[0]["serie"];
                //comprobante.GVComprobante_Folio = (string)DT.Rows[0]["folio"];
                //comprobante.GVComprobante_Importe = Convert.ToDecimal(DT.Rows[0]["importe"]);

                //comprobante.GVComprobante_Pdf = (byte[])(factura[0].PDFFile.Length > 0 ? factura[0].PDFFile : null);
                //comprobante.GVComprobante_Xml = (byte[])(factura[0].XMLFile.Length > 0 ? factura[0].XMLFile : null);
                //comprobante.GVComprobante_Serie = (string)DT.Rows[0]["serie"];
                //comprobante.GVComprobante_Folio = (string)DT.Rows[0]["folio"];
                //comprobante.GVComprobante_Importe = Convert.ToDecimal(DT.Rows[0]["importe"]);
                //comprobante.GVComprobante_GV_Cuenta = TxtCuenta.Text.Trim();
                //comprobante.GVComprobante_GV_Cc = TxtCc.Text.Trim();
                //comprobante.GVComprobante_GV_CuentaPago = TxtCuentaPago.Text.Trim();

                //comprobante.GVComprobante_GV_Numero = TxtNumero.Text.Trim();
                //comprobante.GVComprobante_GV_SubCuenta = TxtSubCuenta.Text.Trim();
                //comprobante.GVComprobante_GV_SubSubCuenta = TxtSubSubCuenta.Text.Trim();

                //comprobante.GVComprobante_XmlStream = factura[0].XMLFile.Length > 0 ? (new System.IO.MemoryStream(factura[0].XMLFile)).ToArray() : null;

                //list.Add(comprobante);


                DataRow drValues = dtValues.NewRow();
                drValues["Id_ConceptoVariable"] = partidarow;
                drValues["Concepto_Descripcion"] = txtConcepto.Text;
                drValues["Concepto_Observaciones"] = txtDescripcion.Text;

                drValues["Concepto_Operador"] = cmbOperador.SelectedItem.Text;
                if (Convert.ToInt32(cmbVariables.SelectedIndex) > 17)
                {
                    drValues["Concepto_TipoVariable"] = 1;
                    drValues["Concepto_VariableDescripcion"] = cmbVariables.Text;
                }
                else
                {
                    drValues["Concepto_TipoVariable"] = 0;
                    drValues["Concepto_VariableDescripcion"] = svariable;
                }
                drValues["Concepto_IdVariable"] = Convert.ToInt32(cmbVariables.SelectedIndex);


                dtValues.Rows.Add(drValues);//adding new row into datatable
                dtValues.AcceptChanges();

                partidarow += 1;

                Session["Table"] = dtValues;


                rgGrid.Rebind();

                cmbVariables.SelectedIndex = 0;
                txtValor.Text = "";



            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void btnRaya_Click(object sender, EventArgs arg)
        {
            try
            {
                this.RadTreeView2.Nodes[0].Nodes.Add(new RadTreeNode("_____________________________________" ));


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void btnNegrita_Click(object sender, EventArgs arg)
        {
            try
            {

                if ( RadTreeView2.SelectedNode.Font.Bold )
                        RadTreeView2.SelectedNode.Font.Bold = false;
                else 
                        RadTreeView2.SelectedNode.Font.Bold = true;
              

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void btnRenglon_Click(object sender, EventArgs arg)
        {
            try
            {
                this.RadTreeView2.Nodes[0].Nodes.Add(new RadTreeNode(txtRenglon.Text));
                this.txtRenglon.Text = "";
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        


        protected void RAM1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }

        protected void btnTerminar_Click(object sender, EventArgs arg)
        {
            try
            {

                if (HD_FormulaPaso.Value == "0")
                {
                    //mandar aviso de que debe poner algo.
                }
                else
                {

                    if ((!String.IsNullOrEmpty(txtConcepto.Text)) && (cmbVariables.FindItemByText(txtConcepto.Text) == null))
                    {
                        int renglones = cmbVariables.Items.Count();
                        cmbVariables.Items.Insert(renglones, new RadComboBoxItem(txtConcepto.Text));
                        cmbVariables.SelectedIndex = 0;

                        this.RadTreeView1.Nodes[0].Nodes[1].Nodes.Add(new RadTreeNode("(" + txtConcepto.Text + ") " + txtDescripcion.Text ));
  
                    }

                    int partidarow = 17;

                    dtValuesVar = (DataTable)Session["TableVar"];
                    foreach (DataRow row in dtValuesVar.Rows)
                    {
                        if (Convert.ToInt32(row["Id_VariableLocal"].ToString()) > partidarow)
                        {
                            partidarow = Convert.ToInt32(row["Id_VariableLocal"].ToString());
                        }
                    }

                    partidarow += 1;


                    DataRow drValuesVar = dtValuesVar.NewRow();
                    drValuesVar["Id_VariableLocal"] = partidarow;
                    drValuesVar["sVariable_Local"] = txtConcepto.Text;
                    drValuesVar["sVariable_Descripcion"] = txtDescripcion.Text;
                    drValuesVar["sVariable_Comentarios"] = txtComentarios.Text;
                    drValuesVar["sVariable_Formula"] = txtFormula.Text;
                    //Poner aqui la formula 



                    //if (Convert.ToInt32(cmbVariables.SelectedIndex) > 17)
                    //{
                    //    drValuesVar["Concepto_TipoVariable"] = 1;
                    //    drValuesVar["Concepto_VariableDescripcion"] = cmbVariables.Text;
                    //}
                    //else
                    //{
                    //    drValuesVar["Concepto_TipoVariable"] = 0;
                    //    drValuesVar["Concepto_VariableDescripcion"] = svariable;
                    //}


                    dtValuesVar.Rows.Add(drValuesVar);//adding new row into datatable
                    dtValuesVar.AcceptChanges();

                    partidarow += 1;

                    Session["TableVar"] = dtValuesVar;


                    grVariables.Rebind();

                    txtArea.Text = txtArea.Text + "\r\n";
                    HD_FormulaPaso.Value = "0";
                    txtConcepto.Text = String.Empty;
                    txtDescripcion.Text = string.Empty;
                    txtFormula.Text = "";
                }


                //rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();
            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }

        protected void rgGrid_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                Int32 item = default(Int32);
                item = e.Item.ItemIndex;

                switch (e.CommandName.ToString())
                {


                    case "Delete":
                        //if (this.rgPagoElectronico.SelectedValue.Count > 0)
                        //id_GVComprobante = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_GVComprobante"].Text);
                        Label lblComprobantedel = e.Item.FindControl("lblId_GVComprobante") as Label;
                        int id_GVComprobantedel = Convert.ToInt32(lblComprobantedel.Text);

                        rgGrid_DeleteCommand(id_GVComprobantedel);
                        //Borrar(id_GVComprobante);
                        break;
                    case "Edit":
                        //if (this.rgPagoElectronico.SelectedValue.Count > 0)
                        //id_GVComprobante = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_GVComprobante"].Text);
                        //RadGrid1_EditCommand(id_GVComprobante);
                        //Borrar(id_GVComprobante);
                        break;
                    case "Modificar":
                        //if (this.rgPagoElectronico.SelectedValue.Count > 0)
                        ////id_GVComprobante = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_GVComprobante"].Text);
                        ////RadGrid1_EditCommand(id_GVComprobante);
                        //Borrar(id_GVComprobante);
                        break;

                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //jfcv 16Nov2016 Inicio que permita editar un registro  punto 5
        protected void rgGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {



                if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    GridEditableItem editItem = (GridEditableItem)e.Item;

                    //obtener nombres de los controles de formulario de inserción/edición de registro de grid.
                    //RadNumericTextBox Ctrl_txtOrd_Cantidad = (RadNumericTextBox)editItem.FindControl("txtFac_Cantidad");
                    //string lblFac_Cantidad = ((Label)editItem.FindControl("lblVal_txtFac_Cantidad")).ClientID.ToString();
                    //string txtFac_Cantidad = Ctrl_txtOrd_Cantidad.ClientID.ToString();
                    //string lbl_cmbProducto = ((Label)editItem.FindControl("lbl_cmbProducto")).ClientID.ToString();
                    //string txtId_Prd = ((RadNumericTextBox)editItem.FindControl("txtId_Prd")).ClientID.ToString();
                    //string lbltxtTerritorioPartida = ((Label)editItem.FindControl("lbltxtTerritorioPartida")).ClientID.ToString();
                    //string txtTerritorioPartida = ((RadNumericTextBox)editItem.FindControl("txtTerritorioPartida")).ClientID.ToString();
                    //string lblTxtClienteExterno = ((Label)editItem.FindControl("lblTxtClienteExterno")).ClientID.ToString();
                    //string txtClienteExterno = ((RadNumericTextBox)editItem.FindControl("txtClienteExterno")).ClientID.ToString();

                    ////Llenar combo de productos
                    //RadComboBox comboProductoItem = (RadComboBox)editItem.FindControl("cmbProducto");
                    ////comboProductoItem.DataSource = this.ListaProductos;
                    ////comboProductoItem.DataBind();
                    ////
                    //CargarProductos(comboProductoItem);
                    //comboProductoItem.SelectedIndex = 0;

                    //Llenar combo de clientes, solo si es movimiento 70
                    //si no, el combo se oculta
                    //RadComboBox comboClientesItem = (RadComboBox)editItem.FindControl("cmbClienteExterno");
                    //RadNumericTextBox txtClienteExternoItem = (RadNumericTextBox)editItem.FindControl("txtClienteExterno");
                    //RadTextBox txtClienteExternoStr = (RadTextBox)editItem.FindControl("txtNombreCliente");
                    ////if (cmbMov.SelectedValue == "70")
                    ////{
                    //    //((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).ReadOnly = false;
                    //    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display = true;
                    //    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExtN")).Display = true;

                    ////}
                    ////else
                    ////{
                    ////    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExtN")).Display = false;
                    ////    ((GridTemplateColumn)rgFacturaDet.Columns.FindByUniqueName("Id_CteExt")).Display = false;


                    ////}


                    //Sesion Sesion = new Sesion();
                    //Sesion = (Sesion)Session["Sesion" + Session.SessionID];



                    //RadComboBox CtaGastosPartidaItem = (RadComboBox)editItem.FindControl("txtcmbCtaGastos");

                    //List<PagoElectronicoCuenta> CtaGastos = new List<PagoElectronicoCuenta>();



                    ////(new CN_CatPagoElectronicoCuenta()).ConsultaCuenta(
                    ////    new PagoElectronicoCuenta() { Id_Emp = Sesion.Id_Emp, Id_Cd = Sesion.Id_Cd_Ver },
                    ////    Sesion.Emp_Cnx,
                    ////    ref CtaGastos
                    ////);

                    //CtaGastosPartidaItem.Items.Clear();
                    //if (CtaGastos.Count > 0)
                    //{


                    //    var datasource = from x in CtaGastos
                    //                     select new
                    //                     {
                    //                         x.Id_Emp,
                    //                         x.Id_Cd,
                    //                         x.Id_PagElecCuenta,
                    //                         x.PagElecCuenta_CC,
                    //                         x.PagElecCuenta_CuentaPago,
                    //                         x.PagElecCuenta_Descripcion,
                    //                         x.PagElecCuenta_Numero,
                    //                         x.PagElecCuenta_SubCuenta,
                    //                         x.PagElecCuenta_SubSubCuenta,
                    //                         DisplayField = String.Format("{0} ({1})", x.PagElecCuenta_Descripcion, x.PagElecCuenta_SubCuenta)
                    //                     };



                    //    CtaGastosPartidaItem.DataSource = datasource;
                    //    CtaGastosPartidaItem.DataValueField = "Id_PagElecCuenta";
                    //    CtaGastosPartidaItem.DataTextField = "DisplayField";
                    //    CtaGastosPartidaItem.DataBind();
                    //}

                    //CtaGastosPartidaItem.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                    //int pagElec_Id_PagElecCuenta = Convert.ToInt32((editItem["Id_PagElec_Id_PagElecCuenta"].FindControl("lblId_PagElec_Id_PagElecCuenta") as Label).Text);



                    //PagoElectronicoCuenta CtaGastos2 = new PagoElectronicoCuenta()
                    //{
                    //    Id_Emp = sesion.Id_Emp,
                    //    Id_Cd = sesion.Id_Cd_Ver,
                    //    Id_PagElecCuenta = Convert.ToInt32(pagElec_Id_PagElecCuenta)
                    //};


                    ////(new CN_CatPagoElectronicoCuenta()).ConsultaCuenta(
                    ////    CtaGastos2,
                    ////    sesion.Emp_Cnx
                    ////);


                    //CtaGastosPartidaItem.SelectedValue = pagElec_Id_PagElecCuenta.ToString();
                    //CtaGastosPartidaItem.Text = CtaGastos2.PagElecCuenta_Descripcion;

                }



            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rgGrid_ItemDataBound");
                //DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaDet_ItemDataBound"));
            }
        }

        protected void rgGrid_InsertCommand(object sender, GridCommandEventArgs e)
        {
            GridEditFormInsertItem insertItem = (GridEditFormInsertItem)e.Item;
            //TextBox txt1 = (TextBox)insertItem["Id_Sistema"].Controls[0];
            TextBox txt2 = (TextBox)insertItem["Id_ConceptoVariable"].Controls[0];


            TextBox txt3 = (TextBox)insertItem["Concepto_Descripcion"].Controls[0];
            TextBox txt4 = (TextBox)insertItem["Concepto_Operador"].Controls[0];
            TextBox txt5 = (TextBox)insertItem["Concepto_TipoVariable"].Controls[0];
            TextBox txt6 = (TextBox)insertItem["Concepto_IdVariable"].Controls[0];
            TextBox txt7 = (TextBox)insertItem["Concepto_VariableDescripcion"].Controls[0];
            TextBox txt8 = (TextBox)insertItem["Concepto_Observaciones"].Controls[0];



            dtValues = (DataTable)Session["Table"];
            DataRow drValues = dtValues.NewRow();
            //drValues["Id_Sistema"] = txt1.Text;
            drValues["Id_ConceptoVariable"] = txt2.Text;
            drValues["Concepto_Descripcion"] = txt3.Text;
            drValues["Concepto_Operador"] = txt4.Text;
            drValues["Concepto_TipoVariable"] = txt5.Text;
            drValues["Concepto_IdVariable"] = txt6.Text;
            drValues["Concepto_VariableDescripcion"] = txt7.Text;
            drValues["Concepto_Observaciones"] = txt8.Text;


            dtValues.Rows.Add(drValues);//adding new row into datatable
            dtValues.AcceptChanges();
            Session["Table"] = dtValues;
            rgGrid.Rebind();

        }

        protected void rgGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //creating datatable
            dtValues = new DataTable();
            //dtValues.Columns.Add("Id_Sistema");
            dtValues.Columns.Add("Id_ConceptoVariable");
            dtValues.Columns.Add("Concepto_Descripcion");
            dtValues.Columns.Add("Concepto_Observaciones");
            dtValues.Columns.Add("Concepto_Operador");
            dtValues.Columns.Add("Concepto_TipoVariable"); //0 es variable definida y 1 variable generada
            dtValues.Columns.Add("Concepto_IdVariable");
            dtValues.Columns.Add("Concepto_VariableDescripcion");

            //DataColumn column2 = new DataColumn("GVComprobante_Xml"); //Create the column.
            //column2.DataType = System.Type.GetType("System.Byte[]"); //Type byte[] to store image bytes.
            //column2.AllowDBNull = true;
            //column2.Caption = "GVComprobante_Xml";
            //dtValues.Columns.Add(column2); //Add the column to the table.


            if (Session["Table"] != null)
            {
                dtValues = (DataTable)Session["Table"];
            }
            rgGrid.DataSource = dtValues;//populate RadGrid with datatable
            Session["Table"] = dtValues;

        }

        protected void rgGrid_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ConceptoVariable concepto = new ConceptoVariable();

                // solo puedo cambiar Operador y Variable
                concepto.Id_ConceptoVariable = Convert.ToInt32((insertedItem["Id_ConceptoVariable"].FindControl("lblId_GVComprobante") as Label).Text);
                concepto.Concepto_Descripcion = "";
                concepto.Concepto_Observaciones = "";
                
                concepto.TipoVariable = 0;
                concepto.IdVariable = 0;
                concepto.VariableDescripcion = "";
                concepto.Id_Cd = sesion.Id_Cd;
                concepto.Id_Emp = sesion.Id_Emp;
                concepto.Id_Sistema = id_Sistema;
                //concepto.Id_ConceptoVariable = Convert.ToInt32(row["Id_ConceptoVariable"]);
                //concepto.Concepto_Descripcion = (string)row["Concepto_Descripcion"];
                //concepto.Concepto_Observaciones = (string)row["Concepto_Observaciones"];
                //concepto.Operador = (string)row["Concepto_Operador"];
                //concepto.TipoVariable = Convert.ToInt32(row["Concepto_TipoVariable"]);
                //concepto.IdVariable = Convert.ToInt32(row["Concepto_IdVariable"]);
                //concepto.VariableDescripcion = (string)row["Concepto_VariableDescripcion"];
                //concepto.FechaElaboracion = DateTime.Now;
 

                
                RadTextBox txtBox = e.Item.FindControl("txtConcepto_VariableDescripcion") as RadTextBox;
                concepto.VariableDescripcion = txtBox.Text;

                RadTextBox txtBoxoperador = e.Item.FindControl("txtConcepto_Operador") as RadTextBox;
                concepto.Operador = txtBoxoperador.Text;


              

                //RadComboBox cmbcuentadegastos = e.Item.FindControl("txtcmbCtaGastos") as RadComboBox;

                //RadComboBox cmbcuentadegastos = (insertedItem["Frc_Tipo"].FindControl("cmbTipo") as RadComboBox);
                //comprobante.GVComprobante_GV_PagElec_Id_PagElecCuenta = cmbcuentadegastos.SelectedValue;


                concepto.Operador = concepto.Operador.Substring(0, 1);
                bool buscar = "()+-*/%".Contains(concepto.Operador);

                if (buscar)
                {
                    dtValues = (DataTable)Session["Table"];
                    foreach (DataRow row in dtValues.Rows)
                    {


                        if (Convert.ToInt32(row["Id_ConceptoVariable"].ToString()) == concepto.Id_ConceptoVariable)
                        {
                            //row["GVComprobante_Observaciones"] = comprobante.GVComprobante_Observaciones;

                            //row["PagElec_Numero"] = comprobante.GVComprobante_GV_Numero;
                            //row["PagElec_Cc"] = comprobante.GVComprobante_GV_Cc;
                            //row["PagElec_Cuenta"] = comprobante.GVComprobante_GV_Cuenta;
                            //row["PagElec_CuentaPago"] = comprobante.GVComprobante_GV_CuentaPago;
                            //row["PagElec_SubCuenta"] = comprobante.GVComprobante_GV_SubCuenta;
                            //row["PagElec_SubSubCuenta"] = comprobante.GVComprobante_GV_SubSubCuenta;
                            //row["PagElec_Id_PagElecCuenta"] = comprobante.GVComprobante_GV_PagElec_Id_PagElecCuenta;

                            //concepto.Concepto_Descripcion = (string)row["Concepto_Descripcion"];
                            //concepto.Concepto_Observaciones = (string)row["Concepto_Observaciones"];
                             row["Concepto_Operador"] = concepto.Operador;
                           
                            //concepto.TipoVariable = Convert.ToInt32(row["Concepto_TipoVariable"]);
                            //concepto.IdVariable = Convert.ToInt32(row["Concepto_IdVariable"]);
                            row["Concepto_VariableDescripcion"] = concepto.VariableDescripcion;
                            //concepto.FechaElaboracion = DateTime.Now;

                        }
                    }

                    dtValues.AcceptChanges();

                    rgGrid.DataSource = dtValues;
                    Session["Table"] = dtValues;
                    rgGrid.Rebind();
                    CargaFormulasAlEditarNew();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgGrid_DeleteCommand(int id_GVComprobante)
        {

            dtValues = (DataTable)Session["Table"];
            // dtValues.Rows[0].Delete();
            DataRow[] drr = dtValues.Select("Id_ConceptoVariable='" + id_GVComprobante + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();

            }

            dtValues.AcceptChanges();


            Session["Table"] = dtValues;
            rgGrid.Rebind();

            CargaFormulasAlEditarNew();
        }
        //JFCV 04-NOV-2016 
        protected void rgGrid_EditCommand(int id_GVComprobante)
        {
            //decimal totalapagar = 0;

            dtValues = (DataTable)Session["Table"];

            //Sesion Sesion = new Sesion();
            //Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            // dtValues.Rows[0].Delete();
            //DataRow[] drr = dtValues.Select("Id_GVComprobante='" + id_GVComprobante + "'");
            //for (int i = 0; i < drr.Length; i++)
            //{
            //if (txtTotalAPagar.Text != "")
            //    totalapagar = Convert.ToDecimal(txtTotalAPagar.Text);

            //totalapagar = totalapagar - Convert.ToDecimal(drr[i]["GVComprobante_Importe"].ToString());
            ////drr[i].Delete();
            //txtTotalAPagar.Text = totalapagar.ToString();
            //TxtImporte.Text = "";


            //this.cmbCtaGastos.SelectedValue = drr[i]["PagElec_Id_PagElecCuenta"].ToString(); //drr[i]["Id_PagElecCuenta"].ToString();
            ///////this.cmbCtaGastos.Text = drr[i]["pagElecCuenta_Descripcion"].ToString();

            //CargarCtaGastos(Convert.ToInt32(drr[i]["PagElec_Id_PagElecCuenta"].ToString()));


            //this.cmbProveedor.SelectedValue = gastoViaje.Id_AcrCheque.ToString();


            //this.cmbProveedor.Text = gastoViaje.AcrCheque_Nombre.ToString();

            //Acreedor provee = new Acreedor() { Id_Emp = Sesion.Id_Emp, Id_Cd = Sesion.Id_Cd_Ver, Id_Acr = gastoViaje.Id_AcrCheque };
            //new CN_CatAcreedor().ConsultaAcreedor(provee, Sesion.Emp_Cnx);

            //txtProveedor.Text = provee.Acr_NumeroGenerado == null ? "Sin Autorizacion" : provee.Acr_NumeroGenerado.ToString().Trim().ToUpper();

            //TxtSolicitante.Text = gastoViaje.PagElec_Solicitante;
            //txtTotalAPagar.Text = gastoViaje.PagElec_Importe.ToString();

            //TxtCuenta.Text = gastoViaje.PagElec_Cuenta;
            //TxtCc.Text = gastoViaje.PagElec_Cc;
            //TxtNumero.Text = gastoViaje.PagElec_Numero;
            //TxtSubCuenta.Text = gastoViaje.PagElec_SubCuenta;
            //TxtSubSubCuenta.Text = gastoViaje.PagElec_SubSubCuenta;
            //TxtCuentaPago.Text = gastoViaje.PagElec_CuentaPago;
            //TxtFechaRequiere.SelectedDate = Convert.ToDateTime(gastoViaje.PagElec_FechaRequiere);
            //TxtObservaciones.Text = gastoViaje.PagElec_Observaciones;


            //this.cmbProveedor.SelectedValue = drr[i]["Id_AcrCheque"].ToString();


            //this.cmbProveedor.Text = drr[i]["AcrCheque_Nombre"].ToString();

            //Acreedor provee = new Acreedor() { Id_Emp = Sesion.Id_Emp, Id_Cd = Sesion.Id_Cd_Ver, Id_Acr = Convert.ToInt32(drr[i]["Id_AcrCheque"]) };
            //new CN_CatAcreedor().ConsultaAcreedor(provee, Sesion.Emp_Cnx);

            //txtProveedor.Text = provee.Acr_NumeroGenerado == null ? "Sin Autorizacion" : provee.Acr_NumeroGenerado.ToString().Trim().ToUpper();

            //TxtSolicitante.Text = drr[i]["PagElec_Solicitante"].ToString();
            //TxtCuenta.Text = drr[i]["PagElec_Cuenta"].ToString();
            //TxtCc.Text = drr[i]["PagElec_Cc"].ToString();
            //TxtNumero.Text = drr[i]["PagElec_Numero"].ToString();
            //TxtSubCuenta.Text = drr[i]["PagElec_SubCuenta"].ToString();
            //TxtSubSubCuenta.Text = drr[i]["PagElec_SubSubCuenta"].ToString();
            //TxtCuentaPago.Text = drr[i]["PagElec_CuentaPago"].ToString();
            //TxtFechaRequiere.SelectedDate = Convert.ToDateTime(drr[i]["PagElec_FechaRequiere"].ToString());
            //TxtObservaciones.Text = drr[i]["GVComprobante_Observaciones"].ToString();

            //TxtImporte.Text = drr[i]["GVComprobante_Importe"].ToString();
            //TxtCuenta.Text = drr[i]["PagElec_Numero"].ToString();
            //TxtCc.Text = drr[i]["PagElec_Cc"].ToString();
            //TxtCuentaPago.Text = drr[i]["PagElec_CuentaPago"].ToString();
            //TxtNumero.Text = drr[i]["PagElec_Numero"].ToString();
            ////PagElec_Cuenta HeaderText="Número" UniqueName="PagElec_Cuenta"
            //TxtSubCuenta.Text = drr[i]["PagElec_SubCuenta"].ToString();
            //TxtSubSubCuenta.Text = drr[i]["PagElec_SubSubCuenta"].ToString();

            //si es de tipo sin comprobante 
            //poner aqui el archivo que se carga etc.
            //comprobante.Id_GVComprobanteTipo = 1;

            //comprobante.GVComprobante_GV_PagElec_Id_PagElecCuenta = cmbCtaGastos.SelectedValue.Trim();

            //File.Delete(Label7.Text);

            //                    <telerik:GridBoundColumn DataField="GVComprobante_Xml" HeaderText="GVComprobante_Xml" UniqueName="GVComprobante_Xml" Visible="false"></telerik:GridBoundColumn>

            //<telerik:GridBoundColumn DataField="GVComprobante_Ruta" HeaderText="GVComprobante_Ruta" UniqueName="GVComprobante_Ruta" Visible="false"></telerik:GridBoundColumn>

            //DataField="Id_GVComprobante" HeaderText="Id" 
            // DataField="PagElec_Rfc" HeaderText="PagElec_Rfc" 

            //DataField="PagElec_Serie"  
            //DataField="PagElec_Folio" HeaderText="Folio"  

            //DataField="PagElec_UUID" HeaderText="PagElec_UUID" UniqueName="PagElec_UUID" Visible="false"></telerik:GridBoundColumn>
            //DataField="PagElec_Subtotal" HeaderText="PagElec_Subtotal" UniqueName="PagElec_Subtotal" Visible="false"></telerik:GridBoundColumn>
            //DataField="PagElec_Iva" HeaderText="PagElec_Iva" UniqueName="PagElec_Iva" Visible="false"></telerik:GridBoundColumn>

            //DateTime? dfecharequiere ;
            //dfecharequiere = Convert.ToDateTime(drr[i]["GVComprobante_Fecha"].ToString());
            //TxtFechaRequiere.SelectedDate= Convert.ToDateTime(dfecharequiere) ;
            //TxtFechaRequiere.SelectedDate = Convert.ToDateTime(.PagElec_FechaRequiere);


            //TODO jfcv cargar si es con comprobante el grid de facturas el de la derecha 
            //    if (Convert.ToString(drr[i]["GVComprobante_ConComprobanteDescripcion"].ToString()) == "Sin Comprobante")
            //    {
            //        ChkConComprobante.Checked = false;


            //        this.CmbTipoComprobanteSin.SelectedIndex = this.CmbTipoComprobanteSin.FindItemIndexByValue("1");
            //        this.CmbTipoComprobanteSin.Text = "Sin Comprobante";
            //        this.CmbTipoComprobanteSin.SelectedValue = "2";

            //        //Use RadComboBoxItem.Selected
            //        RadComboBoxItem item = CmbTipoComprobanteSin.FindItemByText("Sin Comprobante");
            //        item.Selected = true;

            //        //Use RadComboBox.SelectedIndex
            //        int index = CmbTipoComprobanteSin.FindItemIndexByValue("2");
            //        CmbTipoComprobanteSin.SelectedIndex = index;

            //        ////You can also use the SelectedValue property.
            //        //RadComboBox1.SelectedValue = value;


            //        // this.cmbProveedor.SelectedValue = provee.Id_Acr.ToString();
            //        // this.cmbProveedor.Text = provee.Acr_Nombre.ToString();


            //        if (Convert.ToString(drr[i]["GVComprobante_Pdf"].ToString()) != "")
            //        {
            //            //tienesoporte = true;
            //            PagElec_Soporte4 = drr[i]["GVComprobante_Pdf"] == System.DBNull.Value ? null : (byte[])(drr[i]["GVComprobante_Pdf"]);

            //            Label7.Text  = drr[i]["PagElec_Soporte_Nombre"] == System.DBNull.Value ? null : (string)(drr[i]["PagElec_Soporte_Nombre"]);
            //            Label9.Text= Label7.Text;
            //            Label3.Text = drr[i]["PagElec_Soporte_Tipo"] == System.DBNull.Value ? null : (string)(drr[i]["PagElec_Soporte_Tipo"]);
            //            //pagoElectronico.PagElec_SoporteImporte = Convert.ToDecimal(drr[i]["GVComprobante_Importe"].ToString());
            //            RadAsyncUpload1.Visible = false;
            //            btnQuitar.Visible = true;
            //            Label9.Visible = true;

            //        }

            //    }

            //else
            //    {
            //        CmbTipoComprobanteSin.SelectedIndex = 1;
            //        this.CmbTipoComprobanteSin.SelectedValue = "1";
            //        ChkConComprobante.Checked = true;
            //        Label7.Text = "";
            //        Label3.Text = "";
            //        Label9.Text = "";
            //        RadAsyncUpload1.Visible = true;
            //        btnQuitar.Visible = false;
            //        Label9.Visible = false;

            //    }

            //                   if (gastoViaje.PagElec_Soporte != null)

            //decimal totaleapagar = 0;
            //decimal totalpartida = 0;

            //if (txtTotalAPagar.Text != "")
            //    totaleapagar = Convert.ToDecimal(txtTotalAPagar.Text);
            //if (TxtImporte.Text != "")
            //    totalpartida = Convert.ToDecimal(TxtImporte.Text);
            //totaleapagar = totaleapagar + totalpartida;
            //txtTotalAPagar.Text = Convert.ToString(totaleapagar);
            //////rgGrid_DeleteCommand(id_GVComprobante);
            //}

            //dtValues.AcceptChanges();


            //Session["Table"] = dtValues;
            //rgGrid.Rebind();
        }

        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    this.lblMensaje.Text = string.Empty;
                    RadToolBarButton btn = e.Item as RadToolBarButton;
                    if (btn.CommandName == "save")
                    {
                        Guardar();
                    }
                    else if (btn.CommandName == "new")
                    {
                        Nuevo();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick_2");
            }
        }

        protected void Nuevo()
        {
            this.hiddenId.Value = string.Empty;
            cmbOperador.SelectedValue = "0";
            cmbOperador.SelectedIndex = cmbOperador.FindItemIndexByValue("-1");
            if (CmbPerfil.CheckedItems.Count() != 0)
            {
                foreach (RadComboBoxItem rlbi in CmbPerfil.CheckedItems)
                {
                    rlbi.Checked = false;
                }

            }
            cmbVariables.SelectedIndex = cmbVariables.FindItemIndexByValue("-1");

            rgGrid.DataSource = new List<ConceptoVariable>();
            rgGrid.DataBind();
            grVariables.DataSource = new List<ConceptoVariables>();
            grVariables.DataBind();

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            txtNombreSistema.Text = "";
            txtArea.Text = "";
            txtConcepto.Text = "";
            txtDescripcion.Text = "";
            txtFolioSistema.Text = "";
            txtValor.Text = "";
            DateTime fechaElaboracion = DateTime.Now;
            txtFechaInicio.SelectedDate = fechaElaboracion;
            txtFechaFin.SelectedDate = fechaElaboracion;

            Session["Table"] = null;
            Session["TableVar"] = null;
            limpiarGridFormulas();

            RadTreeView2.Nodes.Clear();
            RadTreeNode Node1 = new RadTreeNode("Estado de Resultados Consolidado");
            RadTreeView2.Nodes.Add(Node1);
            
            
            //TODO JFCV crear tabla para llenar formulas y que aqui se recarguen las formulas quitando las varibales 
            // que se hayan creado en este folio 
            


        }

        private void Inicializar()
        {
            DateTime fechaElaboracion = DateTime.Now;
            txtFechaInicio.SelectedDate = fechaElaboracion;
            txtFechaFin.SelectedDate = fechaElaboracion;
            Session["Table"] = null;
            Session["TableVar"] = null;

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            SistemaCompensacion sistemacompensacion = new SistemaCompensacion();

            sistemacompensacion.Id_Emp = Sesion.Id_Emp;
            sistemacompensacion.Id_Cd = Sesion.Id_Cd_Ver;
            sistemacompensacion.Id_Sistema = id_Sistema;
            txtFolioSistema.Text = sistemacompensacion.Id_Sistema.ToString();


            CN_CatCompensacion clsCapPagoElectronico = new CN_CatCompensacion();
            clsCapPagoElectronico.ConsultaConfiguracionSistemacompensacion(sistemacompensacion, Sesion.Emp_Cnx);



            txtNombreSistema.Text = sistemacompensacion.NombreSistema;
            txtFechaInicio.SelectedDate = sistemacompensacion.FechaInicio;
            txtFechaFin.SelectedDate = sistemacompensacion.FechaFin;

            #region actualizar las UEN
            char[] delimiterChars = { ',' };
            CmbPerfil.ClearSelection();
           
            if (CmbPerfil.CheckedItems.Count() != 0)
            {
                foreach (RadComboBoxItem rlbi in CmbPerfil.CheckedItems)
                {
                    rlbi.Checked = false;
                }

            }

            if (sistemacompensacion.Id_Perfil != null)
            {

                string[] words = sistemacompensacion.Id_Perfil.Split(delimiterChars);

                if (sistemacompensacion.Id_Perfil != "0")
                {


                    foreach (string s in words)
                    {
                        CmbPerfil.FindItemByValue(s).Checked = true;

                    }
                }
            }

            #endregion

 

            //INICILIAZAR 
            limpiarGridFormulas();

             
                cargararchivos(sistemacompensacion.listaConceptoVariables);

                cargararchivosVar(sistemacompensacion.listaVariables);

                LlenarTreeView(sistemacompensacion.ListaConceptoVariableReporte);

         
            CargaFormulasNew();
            CargarFormulavar();
            
            //RadTreeNode Node1 = new RadTreeNode("Node1");
            //Node1.Tag = 1234;
            //Node1.BackColor = Color.Blue;
            //RadTreeNode Node2 = new RadTreeNode("Node2");
            //RadTreeNode Node3 = new RadTreeNode("Node3");
            //RadTreeNode Node4 = new RadTreeNode("Node4");
            //radTreeView1.Nodes.Add(Node1);
            //radTreeView1.Nodes.Add(Node2);
            //Node1.Nodes.Add(Node3);
            //Node2.Nodes.Add(Node4);

           



        }

        private void cargararchivos(List<ConceptoVariable> PagElecArchivo)
        {

            int partidarow = 0;

            DataTable dtValues;
            dtValues = (DataTable)Session["Table"];

            if (PagElecArchivo != null)
            {
                foreach (ConceptoVariable componente in PagElecArchivo)
                {

                    DataRow drValues = dtValues.NewRow();
                    drValues["Id_ConceptoVariable"] = componente.Id_ConceptoVariable;
                    drValues["Concepto_Descripcion"] = componente.Concepto_Descripcion;
                    drValues["Concepto_Operador"] = componente.Operador;
                    drValues["Concepto_TipoVariable"] = componente.TipoVariable;
                    drValues["Concepto_IdVariable"] = componente.IdVariable;
                    drValues["Concepto_Observaciones"] = componente.Concepto_Observaciones;
                    drValues["Concepto_VariableDescripcion"] = componente.VariableDescripcion;


                    partidarow += 1;


                    dtValues.Rows.Add(drValues);//adding new row into datatable
                    dtValues.AcceptChanges();

                }
            }

        }

        private void cargararchivosVar(List<ConceptoVariables> PagElecArchivo)
        {

            int partidarow = 0;

            DataTable dtValuesVar;
            dtValuesVar = (DataTable)Session["TableVar"];

            if (PagElecArchivo != null)
            {

                foreach (ConceptoVariables componente in PagElecArchivo)
                {

                    DataRow drValuesVar = dtValuesVar.NewRow();
                    drValuesVar["Id_VariableLocal"] = componente.Id_VariableLocal;
                    drValuesVar["sVariable_Local"] = componente.sVariable_Local;
                    drValuesVar["sVariable_Descripcion"] = componente.sVariable_Descripcion;
                    drValuesVar["sVariable_Comentarios"] = componente.sVariable_Comentarios;
                    drValuesVar["sVariable_Formula"] = componente.sVariable_Formula;

                    partidarow += 1;


                    dtValuesVar.Rows.Add(drValuesVar);//adding new row into datatable
                    dtValuesVar.AcceptChanges();

                }
            }

        }

        private void LlenarTreeView(List<ConceptoVariableReporte> PagElecArchivo)
        {

            int partidarow = 0;


            DataTable dtValuesVarTree = new DataTable();
            //dtValues.Columns.Add("Id_Sistema");
            dtValuesVarTree.Columns.Add("Id_VariableReporte");
            dtValuesVarTree.Columns.Add("Id_Parent");
            dtValuesVarTree.Columns.Add("Id_VariableLocal");
            dtValuesVarTree.Columns.Add("EsBold");
            dtValuesVarTree.Columns.Add("sVariable_Descripcion");
            //drValuesVar["sVariable_Comentarios"] = txtComentarios.Text;
            //drValuesVar["sVariable_Formula"] = txtComentarios.Text;

  
            Session["dtValuesVarTree"] = dtValuesVarTree;

            RadTreeView2.Nodes.Clear();


            if (PagElecArchivo != null)
            {
                foreach (ConceptoVariableReporte componente in PagElecArchivo)
                {

                    DataRow drValuesVar2 = dtValuesVarTree.NewRow();
                    drValuesVar2["Id_VariableReporte"] = componente.Id_VariableReporte;
                    drValuesVar2["Id_Parent"] = componente.Id_Parent;
                    drValuesVar2["Id_VariableLocal"] = componente.Id_VariableLocal;
                    drValuesVar2["EsBold"] = componente.EsBold;
                    drValuesVar2["sVariable_Descripcion"] = componente.sVariable_Descripcion;

                    partidarow += 1;


                    dtValuesVarTree.Rows.Add(drValuesVar2);//adding new row into datatable
                    dtValuesVarTree.AcceptChanges();

                }


                DataSet ds = new DataSet();
                ds.Tables.Add(dtValuesVarTree);
                ds.Relations.Add("NodeRelation", ds.Tables[0].Columns["Id_VariableReporte"], ds.Tables[0].Columns
                ["Id_Parent"]);
                foreach (DataRow dbRow in ds.Tables[0].Rows)
                {
                    if (dbRow.IsNull("Id_Parent"))
                    {
                        RadTreeNode node = CreateNode(dbRow["sVariable_Descripcion"].ToString(), true);
                        node.Font.Bold = Convert.ToBoolean(dbRow["EsBold"]);
                        node.Value = dbRow["Id_VariableLocal"].ToString();
                        RadTreeView2.Nodes.Add(node);
                        RecursivelyPopulate(dbRow, node);
                    }
                }


            }
            else
            {
                RadTreeNode Node1 = new RadTreeNode("Estado de Resultados Consolidado");
                RadTreeView2.Nodes.Add(Node1);
            }

        }


        private void RecursivelyPopulate(DataRow dbRow, RadTreeNode node)
        {
            foreach (DataRow childRow in dbRow.GetChildRows("NodeRelation"))
            {
                RadTreeNode childNode = CreateNode(childRow["sVariable_Descripcion"].ToString(), true);
                node.Nodes.Add(childNode);
                RecursivelyPopulate(childRow, childNode);
            }
        }
        private RadTreeNode CreateNode(string text, bool expanded)
        {
            RadTreeNode node = new RadTreeNode(text);
            node.Expanded = true;
            return node;
        }


        private void limpiarGridFormulas()
        {
            dtValues = new DataTable();
            //dtValues.Columns.Add("Id_Sistema");
            dtValues.Columns.Add("Id_ConceptoVariable");
            dtValues.Columns.Add("Concepto_Descripcion");
            dtValues.Columns.Add("Concepto_Observaciones");
            dtValues.Columns.Add("Concepto_Operador");
            dtValues.Columns.Add("Concepto_TipoVariable"); //0 es variable definida y 1 variable generada
            dtValues.Columns.Add("Concepto_IdVariable");
            dtValues.Columns.Add("Concepto_VariableDescripcion");



            if (Session["Table"] != null)
            {
                dtValues = (DataTable)Session["Table"];
            }
            rgGrid.DataSource = dtValues;//populate RadGrid with datatable
            Session["Table"] = dtValues;

            dtValuesVar = new DataTable();
            //dtValues.Columns.Add("Id_Sistema");
            dtValuesVar.Columns.Add("Id_VariableLocal");
            dtValuesVar.Columns.Add("sVariable_Local");
            dtValuesVar.Columns.Add("sVariable_Descripcion");
            dtValuesVar.Columns.Add("sVariable_Comentarios");
            dtValuesVar.Columns.Add("sVariable_Formula");

            if (Session["TableVar"] != null)
            {
                dtValuesVar = (DataTable)Session["TableVar"];
            }
            grVariables.DataSource = dtValuesVar;//populate RadGrid with datatable
            Session["TableVar"] = dtValuesVar;

         


            dtValues = (DataTable)Session["Table"];
            if (dtValues.Rows.Count > 0)
            {
                rgGrid.DataSource = dtValues;
                rgGrid.DataBind();
            }

            dtValuesVar = (DataTable)Session["TableVar"];
            if (dtValuesVar.Rows.Count > 0)
            {
                grVariables.DataSource = dtValuesVar;
                grVariables.DataBind();
            }

        }


        #region Guardar
        private void Guardar()
        {
           
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            SistemaCompensacion compensacion = new SistemaCompensacion();
            if (txtNombreSistema.Text.Trim() == "")
            {
                Alerta("Por favor Teclee un Nombre de Sistema.");
                return;
            }
            compensacion.NombreSistema = txtNombreSistema.Text;
            compensacion.Id_Perfil = "";
            if (CmbPerfil.CheckedItems.Count() != 0)
            {
                foreach (RadComboBoxItem rlbi in CmbPerfil.CheckedItems)
                {
                    if (compensacion.Id_Perfil == "")
                        compensacion.Id_Perfil = rlbi.Value;
                    else
                        compensacion.Id_Perfil = compensacion.Id_Perfil + "," + rlbi.Value;
                }

            }
 

            CapaDatos.dbAccess oDB = new CapaDatos.dbAccess(Sesion.Emp_Cnx);

            try
            {
                #region concepto variables

                dtValues = (DataTable)Session["Table"];
                if (dtValues.Rows.Count == 0)
                {
                    Alerta("Debe incluir las formulas.");
                    return;
                }


                compensacion.Id_Emp = Sesion.Id_Emp;
                compensacion.Id_Cd = Sesion.Id_Cd_Ver;
                compensacion.Estatus = Convert.ToInt32(chkEstatus.Checked);
                compensacion.FechaInicio = txtFechaInicio.SelectedDate;
                compensacion.FechaFin = txtFechaFin.SelectedDate;


                if (hiddenId.Value != string.Empty)
                {
                    id_Sistema = Convert.ToInt32(hiddenId.Value);
                }
                else
                {
                    id_Sistema = 0;
                }
                compensacion.Id_Sistema = id_Sistema;

                List<ConceptoVariable> listaProdPre = new List<ConceptoVariable>();

                foreach (DataRow row in dtValues.Rows)
                {

                    ConceptoVariable concepto = new ConceptoVariable();
                    concepto.Id_Cd = compensacion.Id_Cd;
                    concepto.Id_Emp = compensacion.Id_Emp;
                    concepto.Id_Sistema = id_Sistema;
                    concepto.Id_ConceptoVariable = Convert.ToInt32(row["Id_ConceptoVariable"]);
                    concepto.Concepto_Descripcion = (string)row["Concepto_Descripcion"];
                    concepto.Concepto_Observaciones = (string)row["Concepto_Observaciones"];
                    concepto.Operador = (string)row["Concepto_Operador"];
                    concepto.TipoVariable = Convert.ToInt32(row["Concepto_TipoVariable"]);
                    concepto.IdVariable = Convert.ToInt32(row["Concepto_IdVariable"]);
                    concepto.VariableDescripcion = (string)row["Concepto_VariableDescripcion"];
                    concepto.FechaElaboracion = DateTime.Now;
                    listaProdPre.Add(concepto);


                }

                compensacion.listaConceptoVariables = listaProdPre;


                IList<RadTreeNode> allNodes = RadTreeView2.GetAllNodes();
                List<ConceptoVariableReporte> listavariablereporte = new List<ConceptoVariableReporte>();
                for (int i = 0; i < allNodes.Count; i++)
                {
                    ConceptoVariableReporte conceptoReporte = new ConceptoVariableReporte();
                    conceptoReporte.Id_Cd = compensacion.Id_Cd;
                    conceptoReporte.Id_Emp = compensacion.Id_Emp;
                    conceptoReporte.Id_Sistema = id_Sistema;
                    RadTreeNode node = (RadTreeNode)allNodes[i];
                    int parentId = node.ParentNode == null ? -1 : allNodes.IndexOf(node.ParentNode);
                    //command = new OleDbCommand("INSERT into Links([id],parentId,[Text]) values (@ID,@parentId,@Text)", con);
                    conceptoReporte.Id_VariableReporte = i;
                    if (parentId > -1)
                        conceptoReporte.Id_Parent = parentId;
                    else
                        conceptoReporte.Id_Parent = null;
                    conceptoReporte.sVariable_Descripcion = node.Text;
                    conceptoReporte.EsBold = node.Font.Bold;
                    if (node.Value != "")
                        conceptoReporte.Id_VariableLocal = Convert.ToInt32(node.Value);
                    else
                        conceptoReporte.Id_VariableLocal = 0;

                    listavariablereporte.Add(conceptoReporte);

                }
                compensacion.ListaConceptoVariableReporte = listavariablereporte;

                #endregion concepto variables

                //for (int i = 0; i < this.listSource.Count; i++)
                //    listaProdPre.Add((ColaboradorObjetivo)this.ClonarPrecioProducto(this.listSource[i]));

                #region variables

                dtValuesVar = (DataTable)Session["TableVar"];
                List<ConceptoVariables> listaVariables = new List<ConceptoVariables>();

                if (dtValuesVar.Rows.Count > 0)
                {


                    foreach (DataRow row in dtValuesVar.Rows)
                    {

                        ConceptoVariables variable = new ConceptoVariables();
                        variable.Id_Cd = compensacion.Id_Cd;
                        variable.Id_Emp = compensacion.Id_Emp;
                        variable.Id_Sistema = id_Sistema;
                        variable.Id_VariableLocal = Convert.ToInt32(row["Id_VariableLocal"]);
                        variable.sVariable_Local = (string)row["sVariable_Local"];
                        variable.sVariable_Descripcion = (string)row["sVariable_Descripcion"];
                        variable.sVariable_Comentarios = (string)row["sVariable_Comentarios"];
                        variable.sVariable_Formula = (string)row["sVariable_Formula"];
                        listaVariables.Add(variable);
                    }

                    compensacion.listaVariables = listaVariables;

                }
                #endregion variables


 

                oDB.BeginTransaction();


                if (hiddenId.Value == string.Empty)
                {

                    compensacion.Estatus = 1;  // si es alta debe ser 1 

                    CN_CatCompensacion clscompensacion = new CN_CatCompensacion();

                    int verificador = 0;
                    clscompensacion.InsertarConfiguracionSistemacompensacion(compensacion, Sesion.Emp_Cnx, ref verificador);

                    if (verificador > 0)
                    {
                        this.hiddenId.Value = verificador.ToString();
                        //JFCV elimino las facturas que haya tenido grabadas 
                        txtFolioSistema.Text = verificador.ToString();
                        Alerta("Se Genero el Sistema");

                    }

                }
                else
                {

                    CN_CatCompensacion clscompensacion = new CN_CatCompensacion();

                    int verificador = 0;
                    clscompensacion.ModificarConfiguracionSistemacompensacion(compensacion, Sesion.Emp_Cnx, ref verificador);

                    if (verificador > 0)
                    {
                        //this.hiddenId.Value = verificador.ToString();
                        Alerta("Se modifico el Sistema");

                    }
                }






                // falta que le cambie el estatus y tipo al gasto de sin comprobantes a con comprobantes 
                // para ello deberia correr un update al gasto y depende si es con comprobantes o sin comprobantes 
                // al cargar que cheque si es con o sin y lo marque
                // 
                //if (esanticipoAcreedores == 1 && eraAnticipoAcreedores == 0)
                //{
                //    clsGastoViaje.InsertarGastoViaje(gastoViaje, Sesion.Emp_Cnx, ref verificador);
                //}

                //clsGastoViaje.ModificarGastoViaje(gastoViaje, Sesion.Emp_Cnx, ref verificador);






                oDB.Commit();

                //Nuevo();

            }

            catch (Exception ex)
            {
                oDB.RollBack();

                ErrorManager(ex, "Guardar_click");

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                oDB.RollBack();


            }

        }
        #endregion Guardar

        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }


        #region grVariables grid variables

        protected void grVariables_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                Int32 item = default(Int32);
                item = e.Item.ItemIndex;

                switch (e.CommandName.ToString())
                {


                    case "Delete":

                        Label lblComprobantedel = e.Item.FindControl("lblId_VariableLocal") as Label;
                        int id_GVComprobantedel = Convert.ToInt32(lblComprobantedel.Text);

                        grVariables_DeleteCommand(id_GVComprobantedel);
                        //Borrar(id_GVComprobante);
                        break;
                    case "Edit":
                        //if (this.rgPagoElectronico.SelectedValue.Count > 0)
                        //id_GVComprobante = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_GVComprobante"].Text);
                        //RadGrid1_EditCommand(id_GVComprobante);
                        //Borrar(id_GVComprobante);
                        break;
                    case "Modificar":
                        //if (this.rgPagoElectronico.SelectedValue.Count > 0)
                        ////id_GVComprobante = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_GVComprobante"].Text);
                        ////RadGrid1_EditCommand(id_GVComprobante);
                        //Borrar(id_GVComprobante);
                        break;

                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        //jfcv 16Nov2016 Inicio que permita editar un registro  punto 5
        protected void grVariables_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                ErrorManager(ex, "grVariables_ItemDataBound");
                //DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaDet_ItemDataBound"));
            }
        }




        protected void grVariables_InsertCommand(object sender, GridCommandEventArgs e)
        {

            try
            {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
          
            GridEditFormInsertItem insertItem = (GridEditFormInsertItem)e.Item;
            //TextBox txt1 = (TextBox)insertItem["Id_Sistema"].Controls[0];
            //TextBox txt2 = (TextBox)insertItem["Id_VariableLocal"].Controls[0];
            //TextBox txt3 = (TextBox)insertItem["sVariable_Local"].Controls[0];
            //TextBox txt4 = (TextBox)insertItem["sVariable_Descripcion"].Controls[0];
            //TextBox txt5 = (TextBox)insertItem["sVariable_Comentarios"].Controls[0];
            //TextBox txt6 = (TextBox)insertItem["sVariable_Formula"].Controls[0];
 
            Hashtable newValues = new Hashtable();

            int partidarow = 17;

            dtValuesVar = (DataTable)Session["TableVar"];
            foreach (DataRow row in dtValuesVar.Rows)
            {
                if (Convert.ToInt32(row["Id_VariableLocal"].ToString()) > partidarow)
                {
                    partidarow = Convert.ToInt32(row["Id_VariableLocal"].ToString());
                }
            }

            partidarow++;

            //newValues["Id_VariableLocal"] = (userControl.FindControl("TextBox7") as TextBox).Text;
            newValues["Id_VariableLocal"] = partidarow.ToString();
            newValues["sVariable_Local"] = (userControl.FindControl("txtConcepto") as RadTextBox).Text;
            newValues["sVariable_Descripcion"] = (userControl.FindControl("txtDescripcion") as RadTextBox).Text;
            newValues["sVariable_Comentarios"] = (userControl.FindControl("txtComentarios") as TextBox).Text;
            newValues["sVariable_Formula"] = (userControl.FindControl("txtFormula") as TextBox).Text;


            dtValuesVar = (DataTable)Session["TableVar"];
            DataRow drValuesVar = dtValuesVar.NewRow();
            //drValues["Id_Sistema"] = txt1.Text;
            drValuesVar["Id_VariableLocal"] = newValues["Id_VariableLocal"];
            drValuesVar["sVariable_Local"] = newValues["sVariable_Local"];
            drValuesVar["sVariable_Descripcion"] = newValues["sVariable_Descripcion"];
            drValuesVar["sVariable_Comentarios"] = newValues["sVariable_Comentarios"];
            drValuesVar["sVariable_Formula"] = newValues["sVariable_Formula"];

            dtValuesVar.Rows.Add(drValuesVar);//adding new row into datatable
            dtValuesVar.AcceptChanges();
            Session["TableVar"] = dtValuesVar;
            grVariables.Rebind();
          

            }
            catch (Exception ex)
            {
                 
            }



            //GridEditFormInsertItem insertItem = (GridEditFormInsertItem)e.Item;
            ////TextBox txt1 = (TextBox)insertItem["Id_Sistema"].Controls[0];
            //TextBox txt2 = (TextBox)insertItem["Id_VariableLocal"].Controls[0];
            //TextBox txt3 = (TextBox)insertItem["sVariable_Local"].Controls[0];
            //TextBox txt4 = (TextBox)insertItem["sVariable_Descripcion"].Controls[0];
            //TextBox txt5 = (TextBox)insertItem["sVariable_Comentarios"].Controls[0];
            //TextBox txt6  = (TextBox)insertItem["sVariable_Formula"].Controls[0];

            //dtValuesVar = (DataTable)Session["TableVar"];
            //DataRow drValuesVar = dtValuesVar.NewRow();
            ////drValues["Id_Sistema"] = txt1.Text;
            //drValuesVar["Id_VariableLocal"] = txt2.Text;
            //drValuesVar["sVariable_Local"] = txt3.Text;
            //drValuesVar["sVariable_Descripcion"] = txt4.Text;
            //drValuesVar["sVariable_Comentarios"] = txt5.Text;
            //drValuesVar["sVariable_Formula"] = txt6.Text;

            //dtValuesVar.Rows.Add(drValuesVar);//adding new row into datatable
            //dtValuesVar.AcceptChanges();
            //Session["TableVar"] = dtValuesVar;
            //grVariables.Rebind();

        }
        protected void grVariables_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //creating datatable
            dtValuesVar = new DataTable();
            //dtValues.Columns.Add("Id_Sistema");
            dtValuesVar.Columns.Add("Id_VariableLocal");
            dtValuesVar.Columns.Add("sVariable_Local");
            dtValuesVar.Columns.Add("sVariable_Descripcion");
            dtValuesVar.Columns.Add("sVariable_Comentarios");
            dtValuesVar.Columns.Add("sVariable_formula");

            if (Session["TableVar"] != null)
            {
                dtValuesVar = (DataTable)Session["TableVar"];
            }
            grVariables.DataSource = dtValuesVar;//populate RadGrid with datatable
            Session["TableVar"] = dtValuesVar;

        }

        protected void grVariables_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            ////try
            ////{
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
               
                GridEditableItem editedItem = e.Item as GridEditableItem;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

                DataTable Employees = new DataTable();
                Employees  =   (DataTable)Session["TableVar"];
              
                dtValues = (DataTable)Session["Table"];


                //Prepare new row to add it in the DataSource
                DataRow[] changedRows = Employees.Select("Id_VariableLocal = " + editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_VariableLocal"]);

                if (changedRows.Length != 1)
                {
                    RadGrid1.Controls.Add(new LiteralControl("Imposible encontrar la actualización de la formula."));
                    e.Canceled = true;
                    return;
                }

                //Update new values
                Hashtable newValues = new Hashtable();

                newValues["Id_VariableLocal"] = (userControl.FindControl("TextBox7") as TextBox).Text;
                newValues["sVariable_Local"] = (userControl.FindControl("txtConcepto") as RadTextBox).Text;
                newValues["sVariable_Descripcion"] = (userControl.FindControl("txtDescripcion") as RadTextBox).Text;
                //newValues["sVariable_Comentarios"] = (userControl.FindControl("HomePhoneBox") as RadMaskedTextBox).Text;
                //newValues["BirthDate"] = (userControl.FindControl("BirthDatePicker") as RadDatePicker).SelectedDate.ToString();
                //newValues["TitleOfCourtesy"] = (userControl.FindControl("ddlTOC") as DropDownList).SelectedItem.Value;

                newValues["sVariable_Comentarios"] = (userControl.FindControl("txtComentarios") as TextBox).Text;
                newValues["sVariable_Formula"] = (userControl.FindControl("txtFormula") as TextBox).Text;
                 

                changedRows[0].BeginEdit();
                try
                {
                    foreach (DictionaryEntry entry in newValues)
                    {
                        changedRows[0][(string)entry.Key] = entry.Value;
                    }
                    changedRows[0].EndEdit();
                    Employees.AcceptChanges();
                    //registros.AcceptChanges();
                    //rgGrid.DataSource = registros;
                    //rgGrid.DataBind();
                    //CargaFormulasAlEditar();

                    dtValues.AcceptChanges();

                    rgGrid.DataSource = dtValues;
                    Session["Table"] = dtValues;
                    rgGrid.Rebind();
                    CargaFormulasNew();
                    CargaFormulasAlEditarNew();


                }
                catch (Exception ex)
                {
                    changedRows[0].CancelEdit();

                    Label lblError = new Label();
                    lblError.Text = "Unable to update Employees. Reason: " + ex.Message;
                    lblError.ForeColor = System.Drawing.Color.Red;
                    RadGrid1.Controls.Add(lblError);

                    e.Canceled = true;
                }








                //////comprobante.Id_GVComprobante = Convert.ToInt32((insertedItem["Id_ConceptoVariable"].FindControl("lblId_GVComprobante") as Label).Text);

                //////RadTextBox txtBox = e.Item.FindControl("txtConcepto_VariableDescripcion") as RadTextBox;
                //////comprobante.GVComprobante_Observaciones = txtBox.Text;

                //////RadComboBox cmbcuentadegastos = e.Item.FindControl("txtcmbCtaGastos") as RadComboBox;

                ////////RadComboBox cmbcuentadegastos = (insertedItem["Frc_Tipo"].FindControl("cmbTipo") as RadComboBox);
                //////comprobante.GVComprobante_GV_PagElec_Id_PagElecCuenta = cmbcuentadegastos.SelectedValue;

                //////PagoElectronicoCuenta CtaGastos = new PagoElectronicoCuenta()
                //////{
                //////    Id_Emp = sesion.Id_Emp,
                //////    Id_Cd = sesion.Id_Cd_Ver,
                //////    Id_PagElecCuenta = Convert.ToInt32(comprobante.GVComprobante_GV_PagElec_Id_PagElecCuenta)
                //////};


                ////(new CN_CatPagoElectronicoCuenta()).ConsultaCuenta(
                ////    CtaGastos,
                ////    sesion.Emp_Cnx
                ////);

                //////comprobante.GVComprobante_GV_Numero = CtaGastos.PagElecCuenta_Numero;
                //////comprobante.GVComprobante_GV_Cc = CtaGastos.PagElecCuenta_CC;
                //////comprobante.GVComprobante_GV_Cuenta = CtaGastos.PagElecCuenta_CC;
                //////comprobante.GVComprobante_GV_Numero = CtaGastos.PagElecCuenta_Numero;
                //////comprobante.GVComprobante_GV_CuentaPago = CtaGastos.PagElecCuenta_CuentaPago;
                //////comprobante.GVComprobante_GV_SubCuenta = CtaGastos.PagElecCuenta_SubCuenta;
                //////comprobante.GVComprobante_GV_SubSubCuenta = CtaGastos.PagElecCuenta_SubSubCuenta;


                //////dtValues = (DataTable)Session["Table"];
                //////foreach (DataRow row in dtValues.Rows)
                //////{


                //////    if (Convert.ToInt32(row["Id_GVComprobante"].ToString()) == comprobante.Id_GVComprobante)
                //////    {
                //////        row["GVComprobante_Observaciones"] = comprobante.GVComprobante_Observaciones;

                //////        row["PagElec_Numero"] = comprobante.GVComprobante_GV_Numero;
                //////        row["PagElec_Cc"] = comprobante.GVComprobante_GV_Cc;
                //////        row["PagElec_Cuenta"] = comprobante.GVComprobante_GV_Cuenta;
                //////        row["PagElec_CuentaPago"] = comprobante.GVComprobante_GV_CuentaPago;
                //////        row["PagElec_SubCuenta"] = comprobante.GVComprobante_GV_SubCuenta;
                //////        row["PagElec_SubSubCuenta"] = comprobante.GVComprobante_GV_SubSubCuenta;
                //////        row["PagElec_Id_PagElecCuenta"] = comprobante.GVComprobante_GV_PagElec_Id_PagElecCuenta;


                //////    }
                //////}

                //////dtValues.AcceptChanges();

                //////rgGrid.DataSource = dtValues;
                //////Session["TableVar"] = dtValues;
                //////rgGrid.Rebind();

                //DataRow[] drr = dtValues.Select("Id_GVComprobante='" + id_GVComprobante + "'");
                //for (int i = 0; i < drr.Length; i++)
                //{
                //    if (txtTotalAPagar.Text != "")
                //        totalapagar = Convert.ToDecimal(txtTotalAPagar.Text);

                //    totalapagar = totalapagar - Convert.ToDecimal(drr[i]["GVComprobante_Importe"].ToString());
                //    drr[i].Delete();
                //    txtTotalAPagar.Text = totalapagar.ToString();
                //    TxtImporte.Text = "";
                //}









                //facturaRevisionCobroDet.Id_Emp = sesion.Id_Emp;
                //facturaRevisionCobroDet.Id_Cd = sesion.Id_Cd_Ver;
                //facturaRevisionCobroDet.Id_Frc = 0; //se debe volver asignar cuando se guarda la orden de compra, cuando actualiza queda igual
                //facturaRevisionCobroDet.Id_FrcDet = 0;
                //facturaRevisionCobroDet.Id_Reg = null;


                //facturaRevisionCobroDet.Frc_Doc =
                //    Convert.ToInt32((insertedItem["Frc_Doc"].FindControl("txtFrc_Doc") as RadNumericTextBox).Text);
                //facturaRevisionCobroDet.Frc_Fecha =
                //    Convert.ToDateTime((insertedItem["Frc_Fecha"].FindControl("txtFrc_Fecha") as RadDatePicker).SelectedDate);
                //facturaRevisionCobroDet.Id_Cte =
                //    Convert.ToInt32((insertedItem["Id_Cte"].FindControl("lblId_CteEdit") as Label).Text);
                //facturaRevisionCobroDet.Cte_NomComercial = (insertedItem["Id_CteStr"].FindControl("lblId_CteStrEdit") as Label).Text;
                //facturaRevisionCobroDet.Frc_Importe =
                //    Convert.ToDouble((insertedItem["Frc_Importe"].FindControl("txtFrc_ImporteEdit") as RadNumericTextBox).Text);
                //facturaRevisionCobroDet.Frc_EnviarA =
                //    Convert.ToInt32((insertedItem["Frc_EnviarA"].FindControl("cmbFrc_EnviarA") as RadComboBox).SelectedValue);
                //facturaRevisionCobroDet.Frc_EnviarAStr = (insertedItem["Frc_EnviarA"].FindControl("cmbFrc_EnviarA") as RadComboBox).SelectedItem.Text;
                //string doc_old = (insertedItem["Frc_Doc"].FindControl("lblVal_txtFrc_Doc") as Label).Text;



                //facturaRevisionCobroDet.Frc_ReqRecibo = Convert.ToInt32((insertedItem["Frc_ReqRecibo"].FindControl("Frc_ReqRecibo") as RadTextBox).Text);

                //if (facturaRevisionCobroDet.Frc_ReqRecibo == 1)
                //    facturaRevisionCobroDet.Frc_MailEnviado = 1;
                //else
                //    facturaRevisionCobroDet.Frc_MailEnviado = 0;

                ////actualizar producto de nota de cargo a la lista
                //this.ListaComprobante_Modificar(gastoViajeComprobante, doc_old);





            ////}
            ////catch (Exception ex)
            ////{
            ////    ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            ////}
        }

        protected void grVariables_DeleteCommand(int id_GVComprobante)
        {
            dtValuesVar = (DataTable)Session["TableVar"];
            // dtValues.Rows[0].Delete();
            DataRow[] drr = dtValuesVar.Select("Id_VariableLocal='" + id_GVComprobante + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();

            }

            dtValuesVar.AcceptChanges();
            Session["TableVar"] = dtValuesVar;
            grVariables.Rebind();


            //elimino las referencias en las formulas 

            //dtValues = (DataTable)Session["Table"];
            //// dtValues.Rows[0].Delete();
            //DataRow[] drr2 = dtValues.Select("Concepto_IdVariable='" + id_GVComprobante + "'");
            //for (int i = 0; i < drr2.Length; i++)
            //{
            //    drr2[i].Delete();

            //}

            //dtValues.AcceptChanges();


            //Session["Table"] = dtValues;
            //rgGrid.Rebind();



           
            CargaFormulasAlEditarNew();


        }
        //JFCV 04-NOV-2016 
        protected void grVariables_EditCommand(int id_GVComprobante)
        {
            //decimal totalapagar = 0;

            dtValuesVar = (DataTable)Session["TableVar"];

            //Sesion Sesion = new Sesion();
            //Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            // dtValues.Rows[0].Delete();
            //DataRow[] drr = dtValues.Select("Id_GVComprobante='" + id_GVComprobante + "'");
            //for (int i = 0; i < drr.Length; i++)
            //{
            //if (txtTotalAPagar.Text != "")
            //    totalapagar = Convert.ToDecimal(txtTotalAPagar.Text);

            //totalapagar = totalapagar - Convert.ToDecimal(drr[i]["GVComprobante_Importe"].ToString());
            ////drr[i].Delete();
            //txtTotalAPagar.Text = totalapagar.ToString();
            //TxtImporte.Text = "";


            //this.cmbCtaGastos.SelectedValue = drr[i]["PagElec_Id_PagElecCuenta"].ToString(); //drr[i]["Id_PagElecCuenta"].ToString();
            ///////this.cmbCtaGastos.Text = drr[i]["pagElecCuenta_Descripcion"].ToString();

            //CargarCtaGastos(Convert.ToInt32(drr[i]["PagElec_Id_PagElecCuenta"].ToString()));


            //this.cmbProveedor.SelectedValue = gastoViaje.Id_AcrCheque.ToString();


            //this.cmbProveedor.Text = gastoViaje.AcrCheque_Nombre.ToString();

            //Acreedor provee = new Acreedor() { Id_Emp = Sesion.Id_Emp, Id_Cd = Sesion.Id_Cd_Ver, Id_Acr = gastoViaje.Id_AcrCheque };
            //new CN_CatAcreedor().ConsultaAcreedor(provee, Sesion.Emp_Cnx);

            //txtProveedor.Text = provee.Acr_NumeroGenerado == null ? "Sin Autorizacion" : provee.Acr_NumeroGenerado.ToString().Trim().ToUpper();

            //TxtSolicitante.Text = gastoViaje.PagElec_Solicitante;
            //txtTotalAPagar.Text = gastoViaje.PagElec_Importe.ToString();

            //TxtCuenta.Text = gastoViaje.PagElec_Cuenta;
            //TxtCc.Text = gastoViaje.PagElec_Cc;
            //TxtNumero.Text = gastoViaje.PagElec_Numero;
            //TxtSubCuenta.Text = gastoViaje.PagElec_SubCuenta;
            //TxtSubSubCuenta.Text = gastoViaje.PagElec_SubSubCuenta;
            //TxtCuentaPago.Text = gastoViaje.PagElec_CuentaPago;
            //TxtFechaRequiere.SelectedDate = Convert.ToDateTime(gastoViaje.PagElec_FechaRequiere);
            //TxtObservaciones.Text = gastoViaje.PagElec_Observaciones;


            //this.cmbProveedor.SelectedValue = drr[i]["Id_AcrCheque"].ToString();


            //this.cmbProveedor.Text = drr[i]["AcrCheque_Nombre"].ToString();

            //Acreedor provee = new Acreedor() { Id_Emp = Sesion.Id_Emp, Id_Cd = Sesion.Id_Cd_Ver, Id_Acr = Convert.ToInt32(drr[i]["Id_AcrCheque"]) };
            //new CN_CatAcreedor().ConsultaAcreedor(provee, Sesion.Emp_Cnx);

            //txtProveedor.Text = provee.Acr_NumeroGenerado == null ? "Sin Autorizacion" : provee.Acr_NumeroGenerado.ToString().Trim().ToUpper();

            //TxtSolicitante.Text = drr[i]["PagElec_Solicitante"].ToString();
            //TxtCuenta.Text = drr[i]["PagElec_Cuenta"].ToString();
            //TxtCc.Text = drr[i]["PagElec_Cc"].ToString();
            //TxtNumero.Text = drr[i]["PagElec_Numero"].ToString();
            //TxtSubCuenta.Text = drr[i]["PagElec_SubCuenta"].ToString();
            //TxtSubSubCuenta.Text = drr[i]["PagElec_SubSubCuenta"].ToString();
            //TxtCuentaPago.Text = drr[i]["PagElec_CuentaPago"].ToString();
            //TxtFechaRequiere.SelectedDate = Convert.ToDateTime(drr[i]["PagElec_FechaRequiere"].ToString());
            //TxtObservaciones.Text = drr[i]["GVComprobante_Observaciones"].ToString();

            //TxtImporte.Text = drr[i]["GVComprobante_Importe"].ToString();
            //TxtCuenta.Text = drr[i]["PagElec_Numero"].ToString();
            //TxtCc.Text = drr[i]["PagElec_Cc"].ToString();
            //TxtCuentaPago.Text = drr[i]["PagElec_CuentaPago"].ToString();
            //TxtNumero.Text = drr[i]["PagElec_Numero"].ToString();
            ////PagElec_Cuenta HeaderText="Número" UniqueName="PagElec_Cuenta"
            //TxtSubCuenta.Text = drr[i]["PagElec_SubCuenta"].ToString();
            //TxtSubSubCuenta.Text = drr[i]["PagElec_SubSubCuenta"].ToString();

            //si es de tipo sin comprobante 
            //poner aqui el archivo que se carga etc.
            //comprobante.Id_GVComprobanteTipo = 1;

            //comprobante.GVComprobante_GV_PagElec_Id_PagElecCuenta = cmbCtaGastos.SelectedValue.Trim();

            //File.Delete(Label7.Text);

            //                    <telerik:GridBoundColumn DataField="GVComprobante_Xml" HeaderText="GVComprobante_Xml" UniqueName="GVComprobante_Xml" Visible="false"></telerik:GridBoundColumn>

            //<telerik:GridBoundColumn DataField="GVComprobante_Ruta" HeaderText="GVComprobante_Ruta" UniqueName="GVComprobante_Ruta" Visible="false"></telerik:GridBoundColumn>

            //DataField="Id_GVComprobante" HeaderText="Id" 
            // DataField="PagElec_Rfc" HeaderText="PagElec_Rfc" 

            //DataField="PagElec_Serie"  
            //DataField="PagElec_Folio" HeaderText="Folio"  

            //DataField="PagElec_UUID" HeaderText="PagElec_UUID" UniqueName="PagElec_UUID" Visible="false"></telerik:GridBoundColumn>
            //DataField="PagElec_Subtotal" HeaderText="PagElec_Subtotal" UniqueName="PagElec_Subtotal" Visible="false"></telerik:GridBoundColumn>
            //DataField="PagElec_Iva" HeaderText="PagElec_Iva" UniqueName="PagElec_Iva" Visible="false"></telerik:GridBoundColumn>

            //DateTime? dfecharequiere ;
            //dfecharequiere = Convert.ToDateTime(drr[i]["GVComprobante_Fecha"].ToString());
            //TxtFechaRequiere.SelectedDate= Convert.ToDateTime(dfecharequiere) ;
            //TxtFechaRequiere.SelectedDate = Convert.ToDateTime(.PagElec_FechaRequiere);


            //TODO jfcv cargar si es con comprobante el grid de facturas el de la derecha 
            //    if (Convert.ToString(drr[i]["GVComprobante_ConComprobanteDescripcion"].ToString()) == "Sin Comprobante")
            //    {
            //        ChkConComprobante.Checked = false;


            //        this.CmbTipoComprobanteSin.SelectedIndex = this.CmbTipoComprobanteSin.FindItemIndexByValue("1");
            //        this.CmbTipoComprobanteSin.Text = "Sin Comprobante";
            //        this.CmbTipoComprobanteSin.SelectedValue = "2";

            //        //Use RadComboBoxItem.Selected
            //        RadComboBoxItem item = CmbTipoComprobanteSin.FindItemByText("Sin Comprobante");
            //        item.Selected = true;

            //        //Use RadComboBox.SelectedIndex
            //        int index = CmbTipoComprobanteSin.FindItemIndexByValue("2");
            //        CmbTipoComprobanteSin.SelectedIndex = index;

            //        ////You can also use the SelectedValue property.
            //        //RadComboBox1.SelectedValue = value;


            //        // this.cmbProveedor.SelectedValue = provee.Id_Acr.ToString();
            //        // this.cmbProveedor.Text = provee.Acr_Nombre.ToString();


            //        if (Convert.ToString(drr[i]["GVComprobante_Pdf"].ToString()) != "")
            //        {
            //            //tienesoporte = true;
            //            PagElec_Soporte4 = drr[i]["GVComprobante_Pdf"] == System.DBNull.Value ? null : (byte[])(drr[i]["GVComprobante_Pdf"]);

            //            Label7.Text  = drr[i]["PagElec_Soporte_Nombre"] == System.DBNull.Value ? null : (string)(drr[i]["PagElec_Soporte_Nombre"]);
            //            Label9.Text= Label7.Text;
            //            Label3.Text = drr[i]["PagElec_Soporte_Tipo"] == System.DBNull.Value ? null : (string)(drr[i]["PagElec_Soporte_Tipo"]);
            //            //pagoElectronico.PagElec_SoporteImporte = Convert.ToDecimal(drr[i]["GVComprobante_Importe"].ToString());
            //            RadAsyncUpload1.Visible = false;
            //            btnQuitar.Visible = true;
            //            Label9.Visible = true;

            //        }

            //    }

            //else
            //    {
            //        CmbTipoComprobanteSin.SelectedIndex = 1;
            //        this.CmbTipoComprobanteSin.SelectedValue = "1";
            //        ChkConComprobante.Checked = true;
            //        Label7.Text = "";
            //        Label3.Text = "";
            //        Label9.Text = "";
            //        RadAsyncUpload1.Visible = true;
            //        btnQuitar.Visible = false;
            //        Label9.Visible = false;

            //    }

            //                   if (gastoViaje.PagElec_Soporte != null)

            //decimal totaleapagar = 0;
            //decimal totalpartida = 0;

            //if (txtTotalAPagar.Text != "")
            //    totaleapagar = Convert.ToDecimal(txtTotalAPagar.Text);
            //if (TxtImporte.Text != "")
            //    totalpartida = Convert.ToDecimal(TxtImporte.Text);
            //totaleapagar = totaleapagar + totalpartida;
            //txtTotalAPagar.Text = Convert.ToString(totaleapagar);
            //////rgGrid_DeleteCommand(id_GVComprobante);
            //}

            //dtValues.AcceptChanges();


            //Session["Table"] = dtValues;
            //rgGrid.Rebind();
        }


        #endregion grVariables grid variables


        private void CargarFormulavar()
        {
 
            dtValuesVar = (DataTable)Session["TableVar"];
            List<ConceptoVariables> listaVariables = new List<ConceptoVariables>();

            if (dtValuesVar.Rows.Count > 0)
            {


                foreach (DataRow row in dtValuesVar.Rows)
                {

                    
                    int id_VariableLocal = Convert.ToInt32(row["Id_VariableLocal"]);
                    string sVariable_Local = (string)row["sVariable_Local"];
                    string sVariable_Descripcion = (string)row["sVariable_Descripcion"];
                    string sVariable_Comentarios = (string)row["sVariable_Comentarios"];
                    string sVariable_Formula = (string)row["sVariable_Formula"];
                   
                    if (!String.IsNullOrEmpty(sVariable_Local) )
                    {
                        int renglones = cmbVariables.Items.Count();
                        cmbVariables.Items.Insert(renglones, new RadComboBoxItem(sVariable_Local));
                        cmbVariables.SelectedIndex = 0;

                        this.RadTreeView1.Nodes[0].Nodes[1].Nodes.Add(new RadTreeNode("(" + sVariable_Local + ") " + sVariable_Descripcion));



                    }

                    
                    
                }

               

            }


        }

        private void CargaFormulas()
        {
 
            #region concepto variables

            dtValues = (DataTable)Session["Table"];
            if (dtValues.Rows.Count == 0)
            {
                txtArea.Text = "";
            }
            else
            {
                txtArea.Text = "";


                List<ConceptoVariable> listaProdPre = new List<ConceptoVariable>();


                string formulanueva = "";


                foreach (DataRow row in dtValues.Rows)
                {
                    if (formulanueva == "")
                    {
                        formulanueva = (string)row["Concepto_Descripcion"] + " = " + (string)row["Concepto_Operador"] + (string)row["Concepto_VariableDescripcion"];
                    }
                    else
                    {
                        formulanueva = formulanueva + (string)row["Concepto_Operador"] + (string)row["Concepto_VariableDescripcion"];
                    }

                    if (row["Concepto_IdVariable"].ToString() == "0")
                    {
                        txtArea.Text = txtArea.Text + formulanueva + "\r\n";
                        formulanueva = "";
                        grVariables.Rebind();
                        HD_FormulaPaso.Value = "0";
                    }

 

                    //concepto.Id_ConceptoVariable = Convert.ToInt32(row["Id_ConceptoVariable"]);

                    //concepto.Concepto_Observaciones = (string)row["Concepto_Observaciones"];

                    //concepto.TipoVariable = Convert.ToInt32(row["Concepto_TipoVariable"]);

                    //concepto.VariableDescripcion = (string)row["Concepto_VariableDescripcion"];

                }

                txtArea.Text = txtArea.Text + formulanueva;

            }

            #endregion concepto variables


        }

        private void CargaFormulasAlEditar()
        {
            //cuando entra aqui ya cargo las formulas y estas ya existen en el grid de grVariables
            //aqui solo entra si edito el grid y hago cambios en la formula.
            //hay que ver como elimino la formula completa 


            #region concepto variables

            string svariable = "";
            string svariableformula = "";


            dtValues = (DataTable)Session["Table"];
            if (dtValues.Rows.Count == 0)
            {
                txtArea.Text = "";
                txtFormula.Text = "";
            }
            else
            {
                txtArea.Text = "";
                List<ConceptoVariable> listaProdPre = new List<ConceptoVariable>();

                string formulanueva = "";
                foreach (DataRow row in dtValues.Rows)
                {

                    if (formulanueva == "")
                    {
                        txtFormula.Text = "";
                    }
                    svariableformula = "";
                    svariable = "";

                    if (Convert.ToString(row["Concepto_Operador"]) == "X" ) 
                        row["Concepto_Operador"] ="*";

                    int caseSwitch = Convert.ToInt32(row["Concepto_Idvariable"]);
                    switch (caseSwitch)
                    {
                        case 0:
                            svariable = "";
                            break;
                        case 1:
                            svariable = "IVC";
                            break;
                        case 2:
                            svariable = "UP";
                            break;
                        case 3:
                            svariable = "ASP";
                            break;
                        case 4:
                            svariable = "GTS";
                            break;
                        case 5:
                            svariable = "AAER";
                            break;
                        case 6:
                            svariable = "MO";
                            break;
                        case 7:
                            svariable = "AEA";
                            break;

                        case 8:
                            svariable = "FC";
                            break;
                        case 9:
                            svariable = "UBC";
                            break;
                        case 10:
                            svariable = "FPPP";
                            break;
                        case 11:
                            svariable = "MVI";
                            break;
                        case 12:
                            svariable = "CND";
                            break;
                        case 13:
                            svariable = "SF";
                            break;
                        case 14:
                            svariable = "TSF";
                            break;
                        case 15:
                            svariable = "GA";
                            break;
                        case 16:
                            svariable = "CB";
                            break;
                        case 17:
                            svariable = Convert.ToString(row["Concepto_VariableDescripcion"]);
                            break;
                        default:
                            svariable = Convert.ToString(row["Concepto_VariableDescripcion"]);
                            //Si eligío una formula entonces tengo que buscar en el grid de formulas la formula de la que eligio y ponerla aqui
                            //if (Convert.ToInt32(row["Concepto_Idvariable"]) == 0) 
                            //{
                            //    svariableformula = Convert.ToString(row["Concepto_Operador"]) ;
                            //}
                            //else
                            //{
                                string formula = "";
                                dtValuesVar = (DataTable)Session["TableVar"];
                                foreach (DataRow rowvs in dtValuesVar.Rows)
                                {
                                    if (Convert.ToString(rowvs["sVariable_Local"]) == svariable)
                                    {
                                        formula = rowvs["sVariable_Formula"].ToString();
                                    }
                                }
                                svariableformula = Convert.ToString(row["Concepto_Operador"]) + formula;
                            //}
                            break;
                    }


                    if (svariableformula == "")
                        svariableformula = Convert.ToString(row["Concepto_Operador"]) + svariable;

                    txtFormula.Text = txtFormula.Text + svariableformula;

                    if (formulanueva == "")
                    {
                        formulanueva = (string)row["Concepto_Descripcion"] + " = " + (string)row["Concepto_Operador"] + (string)row["Concepto_VariableDescripcion"];
                    }
                    else
                    {
                        formulanueva = formulanueva + (string)row["Concepto_Operador"] + (string)row["Concepto_VariableDescripcion"];
                    }

                    if (row["Concepto_IdVariable"].ToString() == "0")
                    {
                        txtArea.Text = txtArea.Text + formulanueva + "\r\n";
                        formulanueva = "";
                        dtValuesVar = (DataTable)Session["TableVar"];
                        foreach (DataRow rowvs2 in dtValuesVar.Rows)
                        {
                            if (Convert.ToString(rowvs2["sVariable_Local"]) == (string)row["Concepto_Descripcion"])
                            {
                                rowvs2["sVariable_Formula"] = txtFormula.Text;
                            }
                        }

                        grVariables.DataSource = dtValuesVar;
                        grVariables.Rebind();
                        Session["TableVar"] = dtValuesVar;
                        txtFormula.Text = "";

                        HD_FormulaPaso.Value = "0";
                        
                    }

                



                    //concepto.Id_ConceptoVariable = Convert.ToInt32(row["Id_ConceptoVariable"]);

                    //concepto.Concepto_Observaciones = (string)row["Concepto_Observaciones"];

                    //concepto.TipoVariable = Convert.ToInt32(row["Concepto_TipoVariable"]);

                    //concepto.VariableDescripcion = (string)row["Concepto_VariableDescripcion"];

                }

                txtArea.Text = txtArea.Text + formulanueva;

            }

            #endregion concepto variables


        }


        private void CargaFormulasNew()
        {

         
            dtValues = (DataTable)Session["Table"];
 
            dtValuesVar =  (DataTable)Session["TableVar"];
            string variablecalcular = "";
            txtArea.Text = "";

              foreach (DataRow rowv in dtValuesVar.Rows)
              {

                    variablecalcular = Convert.ToString(rowv["sVariable_Local"]);
          
                    if (dtValues.Rows.Count > 0)
                    {
              
                                string formulanueva = "";
                                formulanueva = variablecalcular + " = " ;
                           
              
                                DataRow[] result = dtValues.Select("Concepto_Descripcion = '" + variablecalcular + "'");

                                foreach (DataRow row in result)
                                {

                                    formulanueva = formulanueva + (string)row["Concepto_Operador"] + (string)row["Concepto_VariableDescripcion"];

                                }
                                   txtArea.Text = txtArea.Text + formulanueva + "\r\n";
                                   formulanueva = "";
                     }


             }

             


                //rowv["sVariable_Formula"] = txtFormula.Text;
                //txtArea.Text = txtArea.Text + formulanueva + "\r\n";
                //formulanueva = "";
                //grVariables.DataSource = dtValuesVar;
                //grVariables.Rebind();
                //Session["TableVar"] = dtValuesVar;
                //txtFormula.Text = "";


            }

          
 


        private void CargaFormulasAlEditarNew()
        {
            //cuando entra aqui ya cargo las formulas y estas ya existen en el grid de grVariables
            //aqui solo entra si edito el grid y hago cambios en la formula.
            //hay que ver como elimino la formula completa 


            #region concepto variables


            dtValues = (DataTable)Session["Table"];
            dtValuesVar =  (DataTable)Session["TableVar"];
            string variablecalcular = "";
            txtArea.Text = "";

              foreach (DataRow rowv in dtValuesVar.Rows)
              {

                        variablecalcular = Convert.ToString(rowv["sVariable_Local"]);
                       
                        string svariable = "";
                        string svariableformula = "";

                        if (dtValues.Rows.Count == 0)
                        {
                            txtArea.Text = "";
                            txtFormula.Text = "";
                        }
                        else
                        {
                 
                            string formulanueva = "";
                            formulanueva = variablecalcular + " = " ;
                           

                            DataRow[] result = dtValues.Select("Concepto_Descripcion = '" + variablecalcular + "'");

                                                                                                                                                                                                                                                                                                                                                                                                                #region ciclo de operadores 
                        foreach (DataRow row in result)
                        {

                            svariableformula = "";
                            svariable = "";

                            if (Convert.ToString(row["Concepto_Operador"]) == "X")
                                row["Concepto_Operador"] = "*";

                            int caseSwitch = Convert.ToInt32(row["Concepto_Idvariable"]);
                            switch (caseSwitch)
                            {
                                case 0:
                                    svariable = "";
                                    break;
                                case 1:
                                    svariable = "(IVC)";
                                    break;
                                case 2:
                                    svariable = "(UP)";
                                    break;
                                case 3:
                                    svariable = "(ASP)";
                                    break;
                                case 4:
                                    svariable = "(GTS)";
                                    break;
                                case 5:
                                    svariable = "(AAER)";
                                    break;
                                case 6:
                                    svariable = "(MO)";
                                    break;
                                case 7:
                                    svariable = "(AEA)";
                                    break;

                                case 8:
                                    svariable = "(FC)";
                                    break;
                                case 9:
                                    svariable = "(UBC)";
                                    break;
                                case 10:
                                    svariable = "(FPPP)";
                                    break;
                                case 11:
                                    svariable = "(MVI)";
                                    break;
                                case 12:
                                    svariable = "(CND)";
                                    break;
                                case 13:
                                    svariable = "(SF)";
                                    break;
                                case 14:
                                    svariable = "TSF)";
                                    break;
                                case 15:
                                    svariable = "(GA)";
                                    break;
                                case 16:
                                    svariable = "(CB)";
                                    break;
                                case 17:
                                    svariable = Convert.ToString(row["Concepto_VariableDescripcion"]);
                                    break;
                                default:
                                    svariable = Convert.ToString(row["Concepto_VariableDescripcion"]);
                                    //Si eligío una formula entonces tengo que buscar en el grid de formulas la formula de la que eligio y ponerla aqui
                                    //if (Convert.ToInt32(row["Concepto_Idvariable"]) == 0) 
                                    //{
                                    //    svariableformula = Convert.ToString(row["Concepto_Operador"]) ;
                                    //}
                                    //else
                                    //{
                                    string formula = "";
                                    dtValuesVar = (DataTable)Session["TableVar"];
                                    foreach (DataRow rowvs in dtValuesVar.Rows)
                                    {
                                        if (Convert.ToString(rowvs["sVariable_Local"]) == svariable)
                                        {
                                            formula = rowvs["sVariable_Formula"].ToString();
                                        }
                                    }
                                    svariableformula = Convert.ToString(row["Concepto_Operador"]) + "(" + formula + ")";
                                    //}
                                    break;
                            }


                            if (svariableformula == "")
                                svariableformula = Convert.ToString(row["Concepto_Operador"]) + svariable;

                            //if (svariableformula != "(" && svariableformula != ")")
                            //    svariableformula = "(" + svariableformula + ")";
                            txtFormula.Text = txtFormula.Text + svariableformula;

                           
                           formulanueva = formulanueva + (string)row["Concepto_Operador"] + (string)row["Concepto_VariableDescripcion"];
                           
  
                            }   // ciclo de operadores y elementos 

                    #endregion ciclo operadores 
                                rowv["sVariable_Formula"] = txtFormula.Text;
                                txtArea.Text = txtArea.Text + formulanueva + "\r\n";
                                formulanueva = "";
                                grVariables.DataSource = dtValuesVar;
                                grVariables.Rebind();
                                Session["TableVar"] = dtValuesVar;
                                txtFormula.Text = "";

                        }
                } //ciclo de variables 

             

            }

            #endregion concepto variables


        #region otro treeview 
        protected void RadTreeView1_HandleDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {
            RadTreeNode sourceNode = e.SourceDragNode;
            RadTreeNode destNode = e.DestDragNode;
            RadTreeViewDropPosition dropPosition = e.DropPosition;

            if (destNode != null) //drag&drop is performed between trees
            {
                ////if (ChbBetweenNodes.Checked) //dropped node will at the same level as a destination node
                ////{
                    if (sourceNode.TreeView.SelectedNodes.Count <= 1)
                    {
                        PerformDragAndDrop(dropPosition, sourceNode, destNode);
                    }
                    else if (sourceNode.TreeView.SelectedNodes.Count > 1)
                    {
                        if (dropPosition == RadTreeViewDropPosition.Below) //Needed to preserve the order of the dragged items
                        {
                            for (int i = sourceNode.TreeView.SelectedNodes.Count - 1; i >= 0; i--)
                            {
                                PerformDragAndDrop(dropPosition, sourceNode.TreeView.SelectedNodes[i], destNode);
                            }
                        }
                        else
                        {
                            foreach (RadTreeNode node in sourceNode.TreeView.SelectedNodes)
                            {
                                PerformDragAndDrop(dropPosition, node, destNode);
                            }
                        }
                    }
                //////}
                //////else //dropped node will be a sibling of the destination node
                //////{
                //////    if (sourceNode.TreeView.SelectedNodes.Count <= 1)
                //////    {
                //////        if (!sourceNode.IsAncestorOf(destNode))
                //////        {
                //////            sourceNode.Owner.Nodes.Remove(sourceNode);
                //////            destNode.Nodes.Add(sourceNode);
                //////        }
                //////    }
                //////    else if (sourceNode.TreeView.SelectedNodes.Count > 1)
                //////    {
                //////        foreach (RadTreeNode node in RadTreeView1.SelectedNodes)
                //////        {
                //////            if (!node.IsAncestorOf(destNode))
                //////            {
                //////                node.Owner.Nodes.Remove(node);
                //////                destNode.Nodes.Add(node);
                //////            }
                //////        }
                //////    }
                //////}

                destNode.Expanded = true;
                sourceNode.TreeView.UnselectAllNodes();
            }
            
        }

      
        private void AddRowToGrid(DataTable dt, RadTreeNode node)
        {
            string[] values = { node.Text, node.Value };
            dt.Rows.Add(values);
 
        }

        private static void PerformDragAndDrop(RadTreeViewDropPosition dropPosition, RadTreeNode sourceNode,
                                               RadTreeNode destNode)
        {
            if (sourceNode.Equals(destNode) || sourceNode.IsAncestorOf(destNode))
            {
                return;
            }
            sourceNode.Owner.Nodes.Remove(sourceNode);

            switch (dropPosition)
            {
                case RadTreeViewDropPosition.Over:
                    // child
                    if (!sourceNode.IsAncestorOf(destNode))
                    {
                        destNode.Nodes.Add(sourceNode);
                    }
                    break;

                case RadTreeViewDropPosition.Above:
                    // sibling - above                    
                    destNode.InsertBefore(sourceNode);
                    break;

                case RadTreeViewDropPosition.Below:
                    // sibling - below
                    destNode.InsertAfter(sourceNode);
                    break;
            }
        }

        

        protected void ChbMultipleSelect_CheckedChanged(object sender, EventArgs e)
        {
            RadTreeView1.MultipleSelect = !RadTreeView1.MultipleSelect;
            RadTreeView2.MultipleSelect = !RadTreeView2.MultipleSelect;
        }

        protected void ChbBetweenNodes_CheckedChanged(object sender, EventArgs e)
        {
            RadTreeView1.EnableDragAndDropBetweenNodes = !RadTreeView1.EnableDragAndDropBetweenNodes;
            RadTreeView2.EnableDragAndDropBetweenNodes = !RadTreeView2.EnableDragAndDropBetweenNodes;
        }

        #endregion


    }
}