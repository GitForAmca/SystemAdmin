using AMCAPropertiesAdmin.App_Code;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMCAPropertiesAdmin.Admin
{
    public partial class NearByPlaces : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetPlacesFilter(ddlPlaceFilter);  
                FillListView();
            }
        }
        void GetPlacesFilter(DropDownList ddl)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 33;
            GetCommonDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "PlaceName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        } 
        void ClearField()
        {
            ddlPlaceFilter.SelectedIndex = -1;
            txtPlace.Text = "";
        }
        protected void btnSave_Click(object sender, EventArgs e)
        { 

            CommunityPL PL = new CommunityPL();

            var xml = "<tbl>";
            xml += "<tr>";             
            xml += "<PlaceName><![CDATA[" + txtPlace.Text + "]]></PlaceName>"; 
            xml += "</tr>";
            xml += "</tbl>";
            PL.XML = xml;
            PL.CreatedBy = Session["UserAutoId"].ToString(); 

            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 26;
                CommunityDL.returnTable(PL);
            }

            if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 27;
                PL.AutoId = hidPlaceId.Value;
                CommunityDL.returnTable(PL);
            }
            if (!PL.isException)
            {
                divView.Visible = true;
                divEdit.Visible = false;
                ClearField();
                FillListView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record Save Successfully');", true);
            }

            else
            {
                Response.Redirect(Request.RawUrl);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        void FillListView()
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 33;
            PL.AutoId = ddlPlaceFilter.SelectedValue; 
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Places.DataSource = PL.dt;
                LV_Places.DataBind();
            }
            else
            {
                LV_Places.DataSource = "";
                LV_Places.DataBind();
            }
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            divView.Visible = false;
            divEdit.Visible = true;
            ViewState["Mode"] = "Add";
        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_Places.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    hidPlaceId.Value = chkSelect.Attributes["Autoid"];
                    getData(Convert.ToInt32(hidPlaceId.Value)); 
                    ViewState["Mode"] = "Edit";
                    divView.Visible = false;
                    divEdit.Visible = true;
                    break;
                }
            }
        }
        void getData(int id)
        {
            GetCommonPL PL = new GetCommonPL();
            PL.OpCode = 34;
            PL.AutoId = id;
            GetCommonDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                txtPlace.Text = PL.dt.Rows[0]["PlaceName"].ToString(); 
            }
        }  
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divEdit.Visible = false;
            Response.Redirect(Request.RawUrl);
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlPlaceFilter.SelectedIndex = -1; 
            FillListView();
        } 
    }
}