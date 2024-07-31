using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;


namespace SystemAdmin.ESS
{
    public partial class CategoryBox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserAutoId"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            { 
                FillListView();
                GetGroupList(); 
            }
        }  
        void GetGroupList()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 49;
            DropdownDL.returnTable(PL);
            ddlGroupNameFilter.DataValueField = "Id";
            ddlGroupNameFilter.DataTextField = "GroupName";
            ddlGroupNameFilter.DataSource = PL.dt;
            ddlGroupNameFilter.DataBind();
            ddlGroupNameFilter.Items.Insert(0, new ListItem("Select Group Name", ""));

            ddlCategoryGroup.DataValueField = "Id";
            ddlCategoryGroup.DataTextField = "GroupName";
            ddlCategoryGroup.DataSource = PL.dt;
            ddlCategoryGroup.DataBind();
            ddlCategoryGroup.Items.Insert(0, new ListItem("Group Name", ""));
        } 
        void FillListView()
        { 
            //-----------------
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 53;
            PL.AutoId = ddlGroupNameFilter.SelectedValue;
            DropdownDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            LV.DataSource = dt;
            LV.DataBind();
        } 
        void ClearField()
        {
            txtName.Text = ""; 
            ddlCategoryGroup.SelectedIndex = -1;
            hidID.Value = ""; 
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            ClearField();
            divView.Visible = false;
            divAddEdit.Visible = true;
            ViewState["Mode"] = "Add"; 
        } 
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        { 
            foreach (ListViewItem item in LV.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        hidID.Value = Autoid.ToString();
                        setForEdit(Autoid);
                    }
                }
            }
            divView.Visible = false;
            divAddEdit.Visible = true;
            ViewState["Mode"] = "Edit"; 
        }
        void setForEdit(int id)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 50; 
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["CategoryName"].ToString(); 
                ddlCategoryGroup.SelectedIndex = ddlCategoryGroup.Items.IndexOf(ddlCategoryGroup.Items.FindByValue(dt.Rows[0]["GroupId"].ToString())); 
                ViewState["Mode"] = "Edit";
                divView.Visible = false;
                divAddEdit.Visible = true;
            }
            else
            {
                ClearField();
            }
        } 
        protected void btncancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divAddEdit.Visible = false;
        }
        string GetParentServiceXml()
        { 
            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<CategoryName><![CDATA[" + txtName.Text.Trim() + "]]></CategoryName>"; 
            xml += "<GroupId><![CDATA[" + ddlCategoryGroup.SelectedValue + "]]></GroupId>"; 
            xml += "</tr>";
            xml += "</tbl>";
            return xml;
        } 
        protected void btnsave_Click(object sender, EventArgs e)
        { 
            ServiceMasterPL PL = new ServiceMasterPL();
            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 31;
                PL.AutoId = Session["UserAutoId"].ToString();
                PL.XML = GetParentServiceXml();  
                ServiceMasterDL.returnTable(PL);
                if (!PL.isException)
                {
                    divView.Visible = true;
                    divAddEdit.Visible = false;
                    ClearField();
                    FillListView();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record save successfully');", true);
                }
                else
                { 
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                }
            }
            else if (ViewState["Mode"].ToString() == "Edit")
            {
                PL.OpCode = 32;
                PL.XML = GetParentServiceXml();
                PL.AutoId = Convert.ToInt32(hidID.Value);
                ServiceMasterDL.returnTable(PL);
                if (!PL.isException)
                { 
                    divView.Visible = true;
                    divAddEdit.Visible = false;
                    ClearField();
                    FillListView();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record save successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                }
            }
            ClearField(); 
            FillListView(); 
        } 
        protected void ddlGroupNameFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListView();
        }
    }
}