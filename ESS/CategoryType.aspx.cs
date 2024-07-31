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
    public partial class CategoryType : System.Web.UI.Page
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
                DepartmentList();
            }
        }  
        void DepartmentList()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 52;
            DropdownDL.returnTable(PL);
            ddlDepartment.DataValueField = "Id";
            ddlDepartment.DataTextField = "Department";
            ddlDepartment.DataSource = PL.dt;
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", ""));

            ddlDepartmentSearch.DataValueField = "Id";
            ddlDepartmentSearch.DataTextField = "Department";
            ddlDepartmentSearch.DataSource = PL.dt;
            ddlDepartmentSearch.DataBind();
            ddlDepartmentSearch.Items.Insert(0, new ListItem("Select Department", ""));
        } 
        void FillListView()
        {
            string DepartmentString = Request.Form[ddlDepartmentSearch.UniqueID];
            //-----------------
            EssPL PL = new EssPL();
            PL.OpCode = 32;
            PL.Type = ddlTypeSearch.SelectedValue;
            PL.Action = ddlActionSearch.SelectedValue;
            PL.Department = DepartmentString;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            LV.DataSource = dt;
            LV.DataBind();
        } 
        void ClearField()
        {
            txtGroupName.Text = ""; 
            ddlType.SelectedIndex = -1;
            ddlDepartment.SelectedIndex = -1;
            ddlAction.SelectedIndex = -1;
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
            PL.OpCode = 51; 
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            if (dt.Rows.Count > 0)
            {
                txtGroupName.Text = dt.Rows[0]["CategoryGroup"].ToString();
                SetList(ddlDepartment, PL.dt.Rows[0]["Department"].ToString());
                ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByValue(dt.Rows[0]["Type"].ToString()));
                ddlAction.SelectedIndex = ddlAction.Items.IndexOf(ddlAction.Items.FindByValue(dt.Rows[0]["Action"].ToString())); 
                ViewState["Mode"] = "Edit";
                divView.Visible = false;
                divAddEdit.Visible = true;
            }
            else
            {
                ClearField();
            }
        }
        void SetList(ListBox ddl, string ids)
        {
            ddl.SelectedIndex = -1;
            foreach (var item in ids.Split(','))
            {
                foreach (ListItem item2 in ddl.Items)
                {
                    if (item2.Value == item)
                    {
                        item2.Selected = true;
                        break;
                    }
                }
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divAddEdit.Visible = false;
        }
        string GetParentServiceXml()
        {
            string DepartmentString = Request.Form[ddlDepartment.UniqueID];

            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<GroupName><![CDATA[" + txtGroupName.Text.Trim() + "]]></GroupName>"; 
            xml += "<Type><![CDATA[" + ddlType.SelectedValue + "]]></Type>";
            xml += "<Action><![CDATA[" + ddlAction.SelectedValue + "]]></Action>";
            xml += "<Department><![CDATA[" + DepartmentString + "]]></Department>";
            xml += "</tr>";
            xml += "</tbl>";
            return xml;
        } 
        protected void btnsave_Click(object sender, EventArgs e)
        { 
            EssPL PL = new EssPL();
            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 33;
                PL.XML = GetParentServiceXml();  
                EssDL.returnTable(PL);
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
                PL.OpCode = 34;
                PL.XML = GetParentServiceXml();
                PL.AutoId = Convert.ToInt32(hidID.Value);
                EssDL.returnTable(PL);
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
        protected void ddlGroupFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListView();
        } 
        protected void ddlTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListView();
        }

        protected void ddlActionSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListView();
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }

        protected void lstDepartmentSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListView();
        }
    }
}