using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using Telerik.Reporting;
using Telerik.ReportViewer;
using System.Data.Sql;

using Telerik.Web.UI;
using System.Collections;
using CapaEntidad;
using CapaNegocios;
using LibreriaReportes;
using Telerik.Charting;

namespace SIANWEB
{
    public partial class Rep_ClienteAdeudo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            double PorPagar = 0, varPorPagar = 0, Pagado = 0, varPagado = 0;
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion.Id_Cte = int.Parse(Request.QueryString["Id_Cte"]);
                Sesion.Cte_Nombre = Request.QueryString["Cte_Nombre"];
                CN_ClientesMayorAdeudo.RegresaAdeudoCliente(Sesion.Fecha_Cierre,Sesion.Fecha_Corte, Sesion.Dias_Revision, Sesion.Id_Cte, Sesion.Emp_Cnx, ref PorPagar, ref Pagado);

                double total = PorPagar + Pagado;
                varPorPagar = (PorPagar * 100) / total;
                varPagado = (Pagado * 100) / total;

                PieSeriesItem s1 = new PieSeriesItem(Math.Round((decimal)PorPagar, 3), System.Drawing.Color.Red, "Por Cobrar " + Math.Round(varPorPagar, 1).ToString() + "%", false);
                PieSeriesItem s2 = new PieSeriesItem(Math.Round((decimal)Pagado, 3), System.Drawing.Color.Green, "Cobrado " + Math.Round(varPagado, 1).ToString() + "%", true);
                
                DonutSeries ds = new DonutSeries();
                ds.SeriesItems.Add(s1);
                ds.SeriesItems.Add(s2);
                
                ds.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.PieLabelsPosition.Circle;
                RcGrafica.ChartTitle.Text = Sesion.Cte_Nombre + "    " + total.ToString("C2");
                
                ds.LabelsAppearance.DataFormatString = "{0:N2}";
                ds.TooltipsAppearance.Visible = true;
                ds.TooltipsAppearance.DataFormatString = "{0:N2}";

                RcGrafica.PlotArea.Series.Add(ds);
                RcGrafica.DataBind();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}