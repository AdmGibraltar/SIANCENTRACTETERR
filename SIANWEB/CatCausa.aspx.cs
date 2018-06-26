﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SIANWEB
{
    public partial class CatCausa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Empresa");
            DT.Columns.Add("Cau");
            DT.Columns.Add("Desc");

            DT.Rows.Add(new string[] { "", "","" });
            RadGrid1.DataSource = DT;
        }
    }
}