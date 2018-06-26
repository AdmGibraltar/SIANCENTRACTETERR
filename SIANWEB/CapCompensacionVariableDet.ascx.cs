using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using System.Data;
using Telerik.Web.UI;
using System.ComponentModel;

namespace SIANWEB
{
    public partial class CapCompensacionVariableDet : System.Web.UI.UserControl
    {
        private object _dataItem = null;
        DataTable dtValues;
        DataTable dtValuesVar;
        int id_Sistema;


        protected void Page_Load(object sender, System.EventArgs e)
        {
             

             if (hiddenId.Value =="")
            {
               

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                dtValuesVar = (DataTable)Session["TableVar"];
                dtValues = (DataTable)Session["Table"];

 
                int entero = cmbVariables.SelectedIndex;
                try
                {


                    #region Cargar combo 
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

                            if (!String.IsNullOrEmpty(sVariable_Local))
                            {
                                int renglones = cmbVariables.Items.Count();
                                cmbVariables.Items.Insert(renglones, new RadComboBoxItem(sVariable_Local));
                                
                            }

                        }
              
                    }
                    #endregion Cargar Combo

                    if (EsAlta.Value == "True")
                    {
                        hiddenId.Value = txtConcepto.Text;
                    }
                    else
                    {

                        hiddenId.Value = ((System.Data.DataRowView)(DataItem)).Row["sVariable_Local"].ToString();

                        CargaFormulasAlta();
                    }
              
                }
                catch (Exception ex)
                {
                    
                }  

            }

        }

        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }


        #region Grid de renglones
        protected void rgGrid_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                Int32 item = default(Int32);
                item = e.Item.ItemIndex;

                dtValues = (DataTable)Session["Table"];

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
                       
                        ////id_GVComprobante = Convert.ToInt32(rgPagoElectronico.Items[item]["Id_GVComprobante"].Text);
                        try
                        {
                            
                                int elemento = e.Item.ItemIndex+1;

                                HF_ID.Value = dtValues.Rows[item]["Id_ConceptoVariable"].ToString(); 
                                txtComentarios.Text = dtValues.Rows[item]["Concepto_Observaciones"].ToString();

                                #region Operadores

                                string soperadordt = dtValues.Rows[item]["Concepto_Operador"].ToString();

                                //cmbOperador.SelectedValue = soperadorindice;
                                cmbOperador.SelectedIndex = cmbOperador.FindItemIndexByText(soperadordt);

                                #endregion operadores 

                                if (dtValues.Rows[item]["Concepto_IdVariable"].ToString() == "17" || dtValues.Rows[item]["Concepto_IdVariable"].ToString() == "0")
                                {
                                    if (dtValues.Rows[item]["Concepto_IdVariable"].ToString() == "0")
                                    {
                                        cmbVariables.SelectedIndex = 0;
                                    }
                                    else
                                    {
                                        cmbVariables.SelectedValue = "15";
                                    }
                                    txtValor.Text = dtValues.Rows[item]["Concepto_VariableDescripcion"].ToString() ;
                                }
                                else
                                {
                                    if (Convert.ToInt32(dtValues.Rows[item]["Concepto_IdVariable"]) > 17 )
                                    {
                                        
                                       //cmbVariables.SelectedItem.Text = dtValues.Rows[item]["Concepto_VariableDescripcion"].ToString();
                                        cmbVariables.SelectedIndex = cmbVariables.FindItemIndexByText(dtValues.Rows[item]["Concepto_VariableDescripcion"].ToString());

                                    }
                                    else
                                    {
                                        cmbVariables.SelectedIndex = Convert.ToInt32(dtValues.Rows[item]["Concepto_IdVariable"]);
                                    }
                                    txtValor.Text = "";
                                }

                                //if (rgAcreedor.Items[item]["Acr_Nombre"].Text == "&nbsp;")
                                //    TxtNombre.Text = "";
                                //else
                                //    TxtNombre.Text = rgAcreedor.Items[item]["Acr_Nombre"].Text;
                             
                              

                                //GridDataItem dataitem = rgGrid.Items[item];
                                //TableCell cell = dataitem["Acr_Estatus"];
                                //CheckBox checkBox = (CheckBox)cell.Controls[0];

                                btnCancelarEditar.Visible = true;
                                btnEditar.Visible = true;
                                imgBoton.Visible = false;

                                btnCancelarEditar.Enabled = true;
                                btnEditar.Enabled = true;

                           
                        }
                        catch (Exception ex)
                        {
                            ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                        }
              
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


                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    if (item.GetDataKeyValue("Concepto_Descripcion").ToString() == hiddenId.Value) //
                       // ((System.Data.DataRowView)(DataItem)).Row["sVariable_Local"].ToString())  
                    {
                        item.Display = true;  
                    }
                    else
                    {
                        item.Display = false;  
                    }
                }


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
            try{
            //if (hiddenId.Value == "")
            //{
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
               
              
                //GridDataItem item = (GridDataItem)e.Item;
                //if (item.GetDataKeyValue("Concepto_Descripcion").ToString() ==
                //    ((System.Data.DataRowView)(DataItem)).Row["sVariable_Local"].ToString())
                //{
                //    item.Display = true;
                //}
                //else
                //{
                //    item.Display = false;
                //}

                if (dtValues.Rows.Count > 0)
                {
                    foreach (GridDataItem item in rgGrid.Items)
                    {


                        string sVariable_Local = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Concepto_Descripcion"]);
                        if (!String.IsNullOrEmpty(sVariable_Local))
                        {
                            if (sVariable_Local == hiddenId.Value)//((System.Data.DataRowView)(DataItem)).Row["sVariable_Local"].ToString())
                            {
                                item.Display = true;
                            }
                            else
                            {
                                item.Display = false;
                            }

                        }



                    }


                }

                //Session["Table"] = dtValues;
                //rgGrid.DataSource = dtValues;




            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rgGrid_ItemDataBound");
                //DisplayMensajeAlerta(string.Concat(ex.Message, "rgFacturaDet_ItemDataBound"));
            }

            //}
            

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
 
        }
 

        #endregion Grid de Renglones


        protected void btnAgregar_Click(object sender, EventArgs arg)
        {
            try
            {
                string svariable = "";
                string svariableformula = "";
                //if (txtFormula.Text == "")
                //{
                //    txtFormula.Text = "";
                //}


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
                if (cmbVariables.SelectedIndex == 17 && txtValor.Text.Trim() == "")
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



                //if (HD_FormulaPaso.Value == "0")
                //{
                //    txtArea.Text = txtArea.Text + txtConcepto.Text + " = " + cmbOperador.SelectedItem.Text + svariable;
                //    HD_FormulaPaso.Value = txtConcepto.Text + " = " + cmbOperador.SelectedItem.Text + svariable;
                //}
                //else
                //{
                    txtArea.Text = txtArea.Text + cmbOperador.SelectedItem.Text + svariable;
                    HD_FormulaPaso.Value = HD_FormulaPaso.Value + cmbOperador.SelectedItem.Text + svariable;
                //}

                if (svariableformula == "")
                    svariableformula = cmbOperador.SelectedItem.Text + svariable;

                txtFormula.Text = txtFormula.Text + svariableformula;

                

                
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

               
              
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

                rgGrid.DataSource = dtValues;
                rgGrid.Rebind();

                cmbVariables.SelectedIndex = 0;
                txtValor.Text = "";
                //txtConcepto.ReadOnly = true;
                CargaFormulasAlEditarNew();
 

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void btnEditar_Click(object sender, EventArgs arg)
        {
            try
            {
                string svariable = "";
                string svariableformula = "";
         

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
                if (cmbVariables.SelectedIndex == 17 && txtValor.Text.Trim() == "")
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

 
                txtArea.Text = txtArea.Text + cmbOperador.SelectedItem.Text + svariable;
                HD_FormulaPaso.Value = HD_FormulaPaso.Value + cmbOperador.SelectedItem.Text + svariable;
                //}

                if (svariableformula == "")
                    svariableformula = cmbOperador.SelectedItem.Text + svariable;

                txtFormula.Text = txtFormula.Text + svariableformula;




                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];



                int partidarow = 0;

                dtValues = (DataTable)Session["Table"];
                //foreach (DataRow row in dtValues.Rows)
                //{
                //    if (Convert.ToInt32(row["Id_ConceptoVariable"].ToString()) > partidarow)
                //    {
                //        partidarow = Convert.ToInt32(row["Id_ConceptoVariable"].ToString());
                //    }
                //}



                partidarow = Convert.ToInt32(HF_ID.Value)-1;
 
                //dtValues.Rows[partidarow]["Id_ConceptoVariable"] = partidarow;
               
                dtValues.Rows[partidarow]["Concepto_Descripcion"] = txtConcepto.Text;
                dtValues.Rows[partidarow]["Concepto_Observaciones"] = txtDescripcion.Text;
                dtValues.Rows[partidarow]["Concepto_Operador"] = cmbOperador.SelectedItem.Text;

                if (Convert.ToInt32(cmbVariables.SelectedIndex) > 17)
                {
                    dtValues.Rows[partidarow]["Concepto_TipoVariable"] = 1;
                    dtValues.Rows[partidarow]["Concepto_VariableDescripcion"] = cmbVariables.Text;
                }
                else
                {
                    dtValues.Rows[partidarow]["Concepto_TipoVariable"] = 0;
                    dtValues.Rows[partidarow]["Concepto_VariableDescripcion"] = svariable;
                }

                dtValues.Rows[partidarow]["Concepto_IdVariable"] = Convert.ToInt32(cmbVariables.SelectedIndex);
                dtValues.AcceptChanges();
 
                Session["Table"] = dtValues;

                rgGrid.DataSource = dtValues;
                rgGrid.Rebind();

                cmbVariables.SelectedIndex = 0;
                txtValor.Text = "";
                btnCancelarEditar.Visible = false;
                btnEditar.Visible = false;
                imgBoton.Visible = true;
                //txtConcepto.ReadOnly = true;
                CargaFormulasAlEditarNew();
                HF_ID.Value = "0";
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void btnCancelarEditar_Click(object sender, EventArgs arg)
        {
            try
            {
       
                cmbVariables.SelectedIndex = 0;
                txtValor.Text = "";
                btnCancelarEditar.Visible = false;
                btnEditar.Visible = false;
                imgBoton.Visible = true;
             
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

               

                //rgPedido.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
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

                 string formulanueva = hiddenId.Value + " = ";
                txtFormula.Text = "";
             
                foreach (DataRow row in dtValues.Rows)
                {

                    if (Convert.ToString(row["Concepto_Descripcion"]) == hiddenId.Value)
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


                            txtFormula.Text = "";

                            HD_FormulaPaso.Value = "0";

                        }





                        //concepto.Id_ConceptoVariable = Convert.ToInt32(row["Id_ConceptoVariable"]);

                        //concepto.Concepto_Observaciones = (string)row["Concepto_Observaciones"];

                        //concepto.TipoVariable = Convert.ToInt32(row["Concepto_TipoVariable"]);

                        //concepto.VariableDescripcion = (string)row["Concepto_VariableDescripcion"];

                    }
                }

                txtArea.Text = txtArea.Text + formulanueva;

            }

            #endregion concepto variables


        }
        private void CargaFormulasAlta()
        {
            //cuando entra aqui ya cargo las formulas y estas ya existen en el grid de grVariables
            //aqui solo entra si edito el grid y hago cambios en la formula.
            //hay que ver como elimino la formula completa 


            #region concepto variables

            string svariable = "";
            string svariableformula = "";
            


            dtValues = (DataTable)Session["Table"];
            if (dtValues.Rows.Count > 0)
            {
                txtArea.Text = "";
                List<ConceptoVariable> listaProdPre = new List<ConceptoVariable>();

                string formulanueva = hiddenId.Value + " = ";
                foreach (DataRow row in dtValues.Rows)
                {
                    if (Convert.ToString(row["Concepto_Descripcion"])  == hiddenId.Value) 
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

                    //txtFormula.Text = txtFormula.Text + svariableformula;

                    if (formulanueva == "")
                    {
                        formulanueva = (string)row["Concepto_Descripcion"] + " = " + (string)row["Concepto_Operador"] + (string)row["Concepto_VariableDescripcion"];
                    }
                    else
                    {
                        formulanueva = formulanueva + (string)row["Concepto_Operador"] + (string)row["Concepto_VariableDescripcion"];
                    }

                    //if (row["Concepto_IdVariable"].ToString() == "0")
                    //{
                    //    txtArea.Text = txtArea.Text + formulanueva ;
                    //    formulanueva = "";
                    //    //dtValuesVar = (DataTable)Session["TableVar"];
                    //    //foreach (DataRow rowvs2 in dtValuesVar.Rows)
                    //    //{
                    //    //    if (Convert.ToString(rowvs2["sVariable_Local"]) == (string)row["Concepto_Descripcion"])
                    //    //    {
                    //    //        rowvs2["sVariable_Formula"] = txtFormula.Text;
                    //    //    }
                    //    //}

                    //    HD_FormulaPaso.Value = "0";

                    //}


                }
                }

                txtArea.Text = txtArea.Text + formulanueva;
                formulanueva = "";
                HD_FormulaPaso.Value = "0";


                //txtArea.Text = txtArea.Text + formulanueva;

            }

            #endregion concepto variables


        }


        private void CargaFormulasAlEditarNew()
        {
            //cuando entra aqui ya cargo las formulas y estas ya existen en el grid de grVariables
            //aqui solo entra si edito el grid y hago cambios en la formula.
            //hay que ver como elimino la formula completa 


          

            dtValues = (DataTable)Session["Table"];
            dtValuesVar = (DataTable)Session["TableVar"];
            string variablecalcular = "";
            txtArea.Text = "";
            txtFormula.Text = "";

            variablecalcular = hiddenId.Value;

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
                    formulanueva = variablecalcular + " = ";


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
                                DataTable dtValuesVar2 = new DataTable();
                                dtValuesVar2 = (DataTable)Session["TableVar"];
                                foreach (DataRow rowvs in dtValuesVar2.Rows)
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


                        formulanueva = formulanueva + (string)row["Concepto_Operador"] + (string)row["Concepto_VariableDescripcion"];


                    }   // ciclo de operadores y elementos 

                    #endregion ciclo operadores
                    //rowv["sVariable_Formula"] = txtFormula.Text;
                    txtArea.Text = txtArea.Text + formulanueva ;
                    formulanueva = "";
                    //DataTable dtValuesVar3 = new DataTable();
                    //dtValuesVar3 = (DataTable)Session["TableVar"];
                    //foreach (DataRow rowvs3 in dtValuesVar3.Rows)
                    //{
                    //    if (Convert.ToString(rowvs3["sVariable_Local"]) == variablecalcular)
                    //    {
                    //        rowvs3["sVariable_Formula"] = txtFormula.Text;
                    //        rowvs3["sVariable_Local"] = variablecalcular;
                    //    }
                    //}

                    //dtValuesVar3.AcceptChanges();
                    //Session["TableVar"] = dtValuesVar3;
                   


                } //ciclo de variables 
            
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
        private void Alerta(string mensaje)
        {
            try
            {
                //RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

    }


}

 