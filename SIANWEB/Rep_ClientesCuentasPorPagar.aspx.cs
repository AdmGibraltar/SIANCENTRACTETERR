using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Reporting;
using Microsoft.Reporting.WebForms;
using Telerik.Reporting.Processing;
using Telerik.Web.UI;
using CapaNegocios;
using CapaEntidad;
using System.Data;
using System.Drawing;
using System.Collections;

namespace SIANWEB
{
    public partial class Rep_ClientesCuentasPorPagar : System.Web.UI.Page
    {
        CobSaldosNiveles datos;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<CobSaldosNiveles> Lista = new List<CobSaldosNiveles>();
                PieSeriesItem si;
                CN_ClientesMayorAdeudo.RegresaCuentasPorPagadas(Sesion.Fecha_Cierre, Sesion.Dias_Revision, Sesion.Id_Cte, Sesion.Emp_Cnx, ref Lista);
                int i = 1;
                float carteratotal = 0, varporcentaje = 0;

                DonutSeries ds = new DonutSeries();

                foreach (CobSaldosNiveles saldo in Lista)
                {
                    carteratotal = carteratotal + saldo.Restante;
                }

                foreach (CobSaldosNiveles saldo in Lista)
                {
                    datos = saldo;
                    varporcentaje = (saldo.Restante * 100) / carteratotal;
                    si = new PieSeriesItem(Math.Round((decimal)saldo.Restante, 3), Regresacolor(saldo.Cdi), (saldo.Cdi + " - " + saldo.Cd_Nombre) + ' ' + Math.Round(varporcentaje, 1).ToString() + " %");
                    ds.SeriesItems.Add(si);
                    i++;
                }

                Random randObj = new Random(DateTime.Now.Millisecond);
                HF_Cve.Value = randObj.Next().ToString();

                RcGrafica.ChartTitle.Text = Sesion.Cte_Nombre + " Pendiente por cobrar   " + carteratotal.ToString("C2");

                ds.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.PieLabelsPosition.Column;
                
                ds.LabelsAppearance.DataFormatString = "{0:N2}";
                ds.TooltipsAppearance.Visible = true;
                ds.TooltipsAppearance.DataFormatString = "{0}";

                RcGrafica.PlotArea.Series.Add(ds);
                RcGrafica.DataBind();
            }

            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public Color Regresacolor(int cdi)
        {
            Color color = new Color();
            if (cdi == 240) { color = Color.FromArgb(141, 180, 227); }
            if (cdi == 220) { color = Color.FromArgb(24, 108, 232); }
            if (cdi == 190) { color = Color.FromArgb(97, 82, 228); }
            if (cdi == 390) { color = Color.FromArgb(51, 51, 255); }
            if (cdi == 430) { color = Color.FromArgb(51, 102, 204); }
            if (cdi == 410) { color = Color.FromArgb(153, 153, 255); }
            if (cdi == 400) { color = Color.FromArgb(93, 147, 255); }
            if (cdi == 310) { color = Color.FromArgb(255, 192, 0); }
            if (cdi == 340) { color = Color.FromArgb(79, 98, 40); }
            if (cdi == 360) { color = Color.FromArgb(0, 204, 153); }
            if (cdi == 610) { color = Color.FromArgb(0, 176, 80); }
            if (cdi == 370) { color = Color.FromArgb(102, 255, 102); }
            if (cdi == 380) { color = Color.FromArgb(0, 204, 0); }
            if (cdi == 150) { color = Color.FromArgb(155, 187, 89); }
            if (cdi == 620) { color = Color.FromArgb(117, 146, 60); }
            if (cdi == 350) { color = Color.FromArgb(194, 214, 154); }
            if (cdi == 110) { color = Color.FromArgb(228, 109, 10); }
            if (cdi == 160) { color = Color.FromArgb(149, 55, 53); }
            if (cdi == 170) { color = Color.FromArgb(250, 192, 144); }
            if (cdi == 180) { color = Color.FromArgb(217, 151, 149); }
            if (cdi == 200) { color = Color.FromArgb(255, 80, 80); }
            if (cdi == 210) { color = Color.FromArgb(192, 80, 77); }
            if (cdi == 230) { color = Color.FromArgb(176, 32, 56); }
            if (cdi == 510) { color = Color.FromArgb(204, 51, 0); }
            if (cdi == 640) { color = Color.FromArgb(165, 0, 33); }
            return color;
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            string cdi = e.Argument.Substring(0, 3);
            CargarDocumentos(cdi);
        }

        public void CargarDocumentos(string cdi)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                ALValorParametrosInternos.Add(sesion.Fecha_Cierre);
                ALValorParametrosInternos.Add(sesion.Fecha_Corte);
                ALValorParametrosInternos.Add(sesion.Dias);
                ALValorParametrosInternos.Add(sesion.Dias_Revision);
                ALValorParametrosInternos.Add(sesion.Vencidos);
                ALValorParametrosInternos.Add(int.Parse(cdi));
                ALValorParametrosInternos.Add(sesion.Id_Cte);
                ALValorParametrosInternos.Add(sesion.Legal);

                Type instance = null;
                instance = typeof(LibreriaReportes.Rep_clienteMayorAdeudoDocPorPagar);
                Session["InternParameter_Values" + Session.SessionID + HF_Cve.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_Cve.Value] = instance.AssemblyQualifiedName;
                RadAjaxManager1.ResponseScripts.Add("AbrirReporteCve('" + HF_Cve.Value + "');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}