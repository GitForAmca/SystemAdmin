using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SystemAdmin.MasterPage
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserAutoId"] == null)
            {
                Response.Redirect("~/Default.aspx", true);
            }
            if (!Page.IsPostBack)
            {
                lblUsername.Text = Session["UserName"].ToString();
            }
        }
    }
}