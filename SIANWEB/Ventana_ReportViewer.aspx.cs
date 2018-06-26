using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using System.Collections;
using Telerik.Reporting;
using Telerik.ReportViewer;


namespace SIANWEB
{
    public partial class Ventana_ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                 
                }
                else
                {
                    if (Page.IsPostBack == false)
                    {                       
                        string reportName;
                        ArrayList ALValorParametrosInternos;
                        Session["Head" + Session.SessionID] = "Reporte";
                        string cve_pagina = "";

                        if (Page.Request.QueryString["cve"] != null)
                        {
                            cve_pagina = Page.Request.QueryString["cve"].ToString();
                            reportName = Session["assembly" + Session.SessionID + cve_pagina].ToString();
                            ALValorParametrosInternos = (ArrayList)Session["InternParameter_Values" + Session.SessionID + cve_pagina];
                        }
                        else
                        {
                            reportName = Session["assembly" + Session.SessionID].ToString();
                            ALValorParametrosInternos = (ArrayList)Session["InternParameter_Values" + Session.SessionID];
                        }
                        if (!string.IsNullOrEmpty(reportName))
                        {
                            Type reportType = Type.GetType(reportName);
                            Report report = (Report)Activator.CreateInstance(reportType);
                            for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                            {
                                report.ReportParameters[i].AllowNull = true;
                                report.ReportParameters[i].Value = ALValorParametrosInternos[i];
                            }
                            this.ReportViewer1.ReportSource  = report;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void Salir()
        {
            try
            {
                string funcion = null;
                funcion = "CloseWindow()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}