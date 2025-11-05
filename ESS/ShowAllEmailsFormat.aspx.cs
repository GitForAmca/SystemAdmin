using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SystemAdmin.App_Code;
using SystemAdmin.GroupStructure;


namespace SystemAdmin.ESS
{
    public partial class ShowAllEmailsFormat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserAutoId"] != null && ViewState["postback"] == null)
            {
                getEmpData();
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        dynamic Content = "";
        void getEmpData()
        {
            string dtContent = "";

            string GroupId = Request.QueryString["GroupId"].ToString();
            
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 47;
            PL.GroupId = GroupId;
            PL.AutoId = 47;
            ServiceMasterDL.returnTable(PL);
            DataTable dt = PL.dt;

            if (dt.Rows.Count > 0)
            { 
                Content += "<table border='0' cellpadding='0' cellspacing='0' class='page' style='width: 800px'>";
                Content += "<tbody>";
                Content += "<tr>";
                Content += "<td><h2 style='text-align: center; display: block; '><u> " + dt.Rows[0]["GroupName"].ToString() + " </u></h2>"; 

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtContent = "<h3 style =\"display: block; font-weight: bold; font-style: italic;\" ><u> " + dt.Rows[i]["EmailName"].ToString() + " </u></h3>";
                    dtContent += "<br /><span style=\"color:blue;font-weight:bold\">Function Name : </span>" + dt.Rows[i]["Name"].ToString(); 
                    dtContent += "<br /><span style=\"color:blue;font-weight:bold\">Name : </span>" + dt.Rows[i]["EmailName"].ToString(); 
                    dtContent += "<br /><span style=\"color:blue;font-weight:bold\">Type : </span>" + dt.Rows[i]["Type"].ToString(); 
                    dtContent += "<br /><span style=\"color:blue;font-weight:bold\">Subject : </span>" + dt.Rows[i]["Subject"].ToString(); 
                    dtContent += "<br /><span style=\"color:blue;font-weight:bold\">To : </span>" + dt.Rows[i]["To"].ToString(); 
                    dtContent += "<br /><span style=\"color:blue;font-weight:bold\">CC : </span>" + dt.Rows[i]["CC"].ToString(); 
                    dtContent += "<br /><span style=\"color:blue;font-weight:bold\">IsActive : </span>" + dt.Rows[i]["IsActive"].ToString(); 
                    dtContent += "<br /><span style=\"color:blue;font-weight:bold\">Description : </span>" + dt.Rows[i]["Description"].ToString(); 
                    dtContent += "<br /><br />" + dt.Rows[i]["Body"].ToString(); 
                    Content += dtContent;
                    Content += "<hr />"; 
                }

                Content += "</td>";
                Content += "</tr>";
                Content += "</tbody>";
                Content += "</table>";

                DivAllActiveEmails.InnerHtml = Content;
            }
            else
            {

            }
        }
    }
}