using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class segunda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SeriesItem Pagado = new SeriesItem(20);
            Pagado.Name = "Pagado";
            Pagado.Exploded = true;

            SeriesItem PorPagar = new SeriesItem(80);
            PorPagar.Name = "PorPagar";            

            PieSeries ps = new PieSeries();
            ps.Items.Add(Pagado);
            ps.Items.Add(PorPagar);
            
            ps.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.PieLabelsPosition.Column;
            ps.LabelsAppearance.DataFormatString = "{0} %";
            ps.TooltipsAppearance.Visible = false;
            ps.TooltipsAppearance.DataFormatString = "{0} %";


            RadHtmlChart.PlotArea.Series.Add(ps);
            RadHtmlChart.DataBind();
        }
    }
}