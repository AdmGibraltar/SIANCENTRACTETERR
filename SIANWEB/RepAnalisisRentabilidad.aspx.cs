using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using Telerik.Web.Design;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using Telerik.Reporting.Processing;


namespace SIANWEB
{
    public partial class RepAnalisisRentabilidad : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;
            }
        }
        #endregion Variables
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.CargarCentros();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        CargarAnoMes();
                        this.TblEncabezado.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        #region Eventos
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

           
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        //this.rgFacturaRuta.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                if (!_PermisoImprimir)
                {
                    this.Alerta("No tiene permisos para ver el reporte");
                    return;
                }
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (Page.IsValid)
                {
                    if (btn.CommandName == "print")
                    {
                        this.Imprimir();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
            }
            CN__Comun comun = new CN__Comun();
            comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            //nuevo();
        }
        //protected void BtnBuscar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (int.Parse(cmbanioini.SelectedValue) > int.Parse(cmbaniofin.SelectedValue))
        //        {
        //            Alerta("El año inicial no puede ser mayor al año final");
        //            return;
        //        }
        //        if (int.Parse(cmbanioini.SelectedValue) == int.Parse(cmbaniofin.SelectedValue))
        //        {
        //            if (int.Parse(cmbmesini.SelectedValue) > int.Parse(cmbmesfin.SelectedValue))
        //            {
        //                Alerta("El mes inicial no puede ser mayor al año final");
        //                return;
        //            }
        //        }



        //        this.RgConsulta.Rebind();
        //        RgConsulta.CollapseAllRowGroups();
        //    }
        //    catch (Exception ex)
        //    {

        //        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        //    }
        //}
        //protected void BtnExportarExcel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        RgConsulta.ExportSettings.IgnorePaging = true;
        //        RgConsulta.ExportToExcel();
        //    }
        //    catch (Exception ex)
        //    {

        //        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        //    }
        //}
        //protected void RadPivotGrid1_NeedDataSource(object sender, Telerik.Web.UI.PivotGridNeedDataSourceEventArgs e)
        //{
        //    try
        //    {
        //        this.RgConsulta.DataSource = getList();
        //        this.RgConsulta.DataBind();
         
        //    }
        //    catch (Exception ex)
        //    {

        //        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
        //    }

        //}
        #endregion Eventos
        #region Funciones
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;

                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                    _PermisoImprimir = Permiso.PImprimir;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void boton(string cadena, ref int error)
        {
            if (!string.IsNullOrEmpty(cadena))
            {
                string[] split = cadena.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] split2;
                foreach (string a in split)
                {
                    if (a.Contains("-"))
                    {
                        split2 = a.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        if (split2.Length != 2)
                        {
                            Alerta("El rango " + a.ToString() + " no es válido");
                            error = 1;
                        }
                        if (split2.Length == 2)
                            if (Convert.ToInt32(split2[0]) > Convert.ToInt32(split2[1]))
                            {
                                Alerta("El rango " + a.ToString() + " no es válido");
                                error = 1;
                            }
                    }
                }
            }
        }
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();


                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAnoMes()
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.Emp_Cnx, "spCatCalendarioAnhio2_Combo", ref this.cmbanioini);
                cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.CalendarioIni.Year, sesion.Emp_Cnx, "spCatCalendarioMes_Combo", ref this.cmbmesini);
                //cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.Emp_Cnx, "spCatCalendarioAnhio2_Combo", ref this.cmbaniofin);
               // cn_comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd, sesion.CalendarioIni.Year, sesion.Emp_Cnx, "spCatCalendarioMes_Combo", ref this.cmbmesfin);
          

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Imprimir()
        {
            try
            {
                if (this.cmbmesini.SelectedValue == "-1")
                {
                    Alerta("Seleccione el mes");
                    return;
                }
                if (this.cmbanioini.SelectedValue == "-1")
                {
                    Alerta("Seleccione el año");
                    return;
                }
               /* if (this.cmbmesfin.SelectedValue == "-1")
                {
                    Alerta("Seleccione el mes final");
                    return;
                }*/
                if (this.cmbanioini.SelectedValue == "-1")
                {
                    Alerta("Seleccione el año");
                    return;
                }


                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();
                String Filtro = "Periodo de " + this.cmbmesini.Text + "-" + cmbanioini.SelectedValue ;

                String CerosMes ="";

                if (this.cmbmesini.SelectedValue.Length == 1)
                {
                    CerosMes = "0";
                }



                if (System.IO.File.Exists(Server.MapPath("~/Reportes") + "\\Rentabilidad" + this.cmbanioini.SelectedValue + CerosMes + this.cmbmesini.SelectedValue + ".xls"))
                {
                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", "Reportes/Rentabilidad" + this.cmbanioini.SelectedValue + CerosMes + this.cmbmesini.SelectedValue + ".xls", "')"));
                }
                else
                {
                    Alerta("No existe Analisis de Rentabilidad para este Periodo");
                }
            



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //private List<SegGestionRentabilidad> getList()
        //{
        //    try
        //    {
        //        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        List<SegGestionRentabilidad> List = new List<SegGestionRentabilidad>();
        //        CN_GestionRentabilidad cn_gr = new CN_GestionRentabilidad();

        //        cn_gr.MonitoreoUB_Central(int.Parse(this.RblTipoCd.SelectedValue), int.Parse(this.cmbmesini.SelectedValue), int.Parse(this.cmbanioini.SelectedValue), int.Parse(this.cmbmesfin.SelectedValue), int.Parse(this.cmbaniofin.SelectedValue), ref List, sesion.Emp_Cnx);

        //        return List;
        //    }
        //    catch (Exception ex)
        //    {
                
        //        throw ex;
        //    }
        //}
        //protected void RadPivotGrid1_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
        //{
       
        //    PivotGridBaseModelCell modelDataCell = e.PivotGridModelCell as PivotGridBaseModelCell;
        //    if (modelDataCell != null)
        //    {
        //        AddStylesToDataCells(modelDataCell, e);
        //    }

        //    if (modelDataCell.TableCellType == PivotGridTableCellType.RowHeaderCell)
        //    {
        //        AddStylesToRowHeaderCells(modelDataCell, e);
        //    }

        //    if (modelDataCell.TableCellType == PivotGridTableCellType.ColumnHeaderCell)
        //    {
        //        AddStylesToColumnHeaderCells(modelDataCell, e);
        //    }

        //    if (modelDataCell.IsGrandTotalCell)
        //    {
        //        e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
        //        e.ExportedCell.Style.Font.Bold = true;
        //    }

        //    if (IsTotalDataCell(modelDataCell))
        //    {
        //        e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(150, 150, 150);
        //        e.ExportedCell.Style.Font.Bold = true;
        //        AddBorders(e);
        //    }

        //    if (IsGrandTotalDataCell(modelDataCell))
        //    {
        //        e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
        //        e.ExportedCell.Style.Font.Bold = true;
        //        AddBorders(e);
        //    }
        //}
        //private void AddStylesToDataCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
        //{
        //    if (modelDataCell.Data != null && modelDataCell.Data.GetType() == typeof(decimal))
        //    {
        //        decimal value = Convert.ToDecimal(modelDataCell.Data);
        //        //if (value > 100000)
        //        //{
        //        //    e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(51, 204, 204);
        //        //    AddBorders(e);
        //        //}

        //        e.ExportedCell.Format = "$0.0";
        //    }
        //}
        //private void AddStylesToColumnHeaderCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
        //{
        //    if (e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width == 0)
        //    {
        //        e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width = 20D;
        //    }

        //    if (modelDataCell.IsTotalCell)
        //    {
        //        e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(150, 150, 150);
        //        e.ExportedCell.Style.Font.Bold = true;
        //    }
        //    else
        //    {
        //        e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        //    }
        //    AddBorders(e);
        //}
        //private void AddStylesToRowHeaderCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
        //{
        //    if (e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width == 0)
        //    {
        //        e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width = 11D;
        //        e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Style.HorizontalAlign   = System.Web.UI.WebControls.HorizontalAlign.Left;
        //    }
        //    if (modelDataCell.IsTotalCell)
        //    {
        //        e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(150, 150, 150);
        //        e.ExportedCell.Style.Font.Bold = true;
        //    }
        //    else
        //    {
        //        e.ExportedCell.Style.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);
        //    }

        //    AddBorders(e);
        //}
        //private static void AddBorders(PivotGridCellExportingArgs e)
        //{
        //    e.ExportedCell.Style.BorderBottomColor = System.Drawing.Color.FromArgb(128, 128, 128);
        //    e.ExportedCell.Style.BorderBottomWidth = new Unit(1);
        //    e.ExportedCell.Style.BorderBottomStyle = BorderStyle.Solid;

        //    e.ExportedCell.Style.BorderRightColor = System.Drawing.Color.FromArgb(128, 128, 128);
        //    e.ExportedCell.Style.BorderRightWidth = new Unit(1);
        //    e.ExportedCell.Style.BorderRightStyle = BorderStyle.Solid;

        //    e.ExportedCell.Style.BorderLeftColor = System.Drawing.Color.FromArgb(128, 128, 128);
        //    e.ExportedCell.Style.BorderLeftWidth = new Unit(1);
        //    e.ExportedCell.Style.BorderLeftStyle = BorderStyle.Solid;

        //    e.ExportedCell.Style.BorderTopColor = System.Drawing.Color.FromArgb(128, 128, 128);
        //    e.ExportedCell.Style.BorderTopWidth = new Unit(1);
        //    e.ExportedCell.Style.BorderTopStyle = BorderStyle.Solid;
        //}
        //private bool IsTotalDataCell(PivotGridBaseModelCell modelDataCell)
        //{
        //    return modelDataCell.TableCellType == PivotGridTableCellType.DataCell &&
        //       (modelDataCell.CellType == PivotGridDataCellType.ColumnTotalDataCell ||
        //         modelDataCell.CellType == PivotGridDataCellType.RowTotalDataCell ||
        //         modelDataCell.CellType == PivotGridDataCellType.RowAndColumnTotal);
        //}
        //private bool IsGrandTotalDataCell(PivotGridBaseModelCell modelDataCell)
        //{
        //    return modelDataCell.TableCellType == PivotGridTableCellType.DataCell &&
        //        (modelDataCell.CellType == PivotGridDataCellType.ColumnGrandTotalDataCell ||
        //            modelDataCell.CellType == PivotGridDataCellType.ColumnGrandTotalRowTotal ||
        //            modelDataCell.CellType == PivotGridDataCellType.RowGrandTotalColumnTotal ||
        //            modelDataCell.CellType == PivotGridDataCellType.RowGrandTotalDataCell ||
        //            modelDataCell.CellType == PivotGridDataCellType.RowAndColumnGrandTotal);
        //}
        //protected void RadPivotGrid1_PivotGridBiffExporting(object sender, PivotGridBiffExportingEventArgs e)
        //{
    
        //        eis.Table newWorksheet = new eis.Table("My New Worksheet");

        //        eis.Cell headerCell = newWorksheet.Cells[1, 1];
        //        headerCell.Value = "Legend";
        //        headerCell.Style.BorderBottomColor = System.Drawing.Color.Black;
        //        headerCell.Style.BorderBottomStyle = BorderStyle.Double;
        //        headerCell.Style.Font.Bold = true;
        //        headerCell.Colspan = 2;
        //        newWorksheet.Columns[1].Width = 32D;
        //        newWorksheet.Cells[1, 2].Value = "Cells";
        //        newWorksheet.Cells[1, 2].Style.Font.Bold = true;
        //        newWorksheet.Cells[2, 2].Value = "Color";
        //        newWorksheet.Cells[2, 2].Style.Font.Bold = true;

        //        newWorksheet.Cells[1, 3].Value = "Cells with values bigger than 100 000";
        //        newWorksheet.Cells[1, 4].Value = "Totals� cells";
        //        newWorksheet.Cells[1, 5].Value = "Grand totals� cells";
        //        newWorksheet.Cells[1, 6].Value = "Row and column header tables� cells";

        //        newWorksheet.Cells[2, 3].Value = "#33CCCC";
        //        newWorksheet.Cells[2, 3].Style.BackColor = System.Drawing.Color.FromArgb(51, 204, 204);
        //        newWorksheet.Cells[2, 4].Value = "#969696";
        //        newWorksheet.Cells[2, 4].Style.BackColor = System.Drawing.Color.FromArgb(150, 150, 150);
        //        newWorksheet.Cells[2, 5].Value = "#808080";
        //        newWorksheet.Cells[2, 5].Style.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
        //        newWorksheet.Cells[2, 6].Value = "#C0C0C0";
        //        newWorksheet.Cells[2, 6].Style.BackColor = System.Drawing.Color.FromArgb(192, 192, 192);

        //        e.ExportStructure.Tables.Add(newWorksheet);
            
        //}

        #endregion Funciones
        #region ErrorManager

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

        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        private void ErrorManager()
        {
            try
            {
                this.lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ErrorManager(string Message)
        {
            try
            {
                this.lblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
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

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta("Error al momento de imprimir");
                else
                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #endregion

      
    }
}