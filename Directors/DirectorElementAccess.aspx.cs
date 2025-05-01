using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.Directors
{
    public partial class DirectorElementAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillListView();
                getDirectorList(ddlDirector);
                getDirectorList(ddlDirectorSearch);
                BindElement();
            }
        }
        void getDirectorList(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 68;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Autoid";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select an option", ""));
        }
        private void BindElement()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 69;
            PL.ServiceTypeAutoid = "6,7,8,9";
            DropdownDL.returnTable(PL);
            ddlElement.DataSource = PL.dt;
            ddlElement.DataValueField = "Element";
            ddlElement.DataTextField = "Element";
            ddlElement.DataBind();
            ddlElement.Items.Insert(0, new ListItem("Select an option", ""));

            DropdownDL.returnTable(PL);
            ddlElementSearch.DataSource = PL.dt;
            ddlElementSearch.DataValueField = "Element";
            ddlElementSearch.DataTextField = "Element";
            ddlElementSearch.DataBind();
            ddlElementSearch.Items.Insert(0, new ListItem("Select an option", ""));
        }
        private void BindEmployee(string Element , DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 70;
            string department = "";
            string subdepartment = "";
            if(Element == "HR")
            {
                department = "13";
            }
            if(Element == "EA")
            {
                department = "44,46,47,48,49";
            }
            if(Element == "Supervisor")
            {
                department = "3";
                subdepartment = "73";
            }
            if(Element == "Coordinator")
            {
                department = "42";
            }
            PL.ServiceTypeAutoid = department;
            PL.SubDepartmentId = subdepartment;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Autoid";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select an option", ""));
        }
        protected void ddlElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEmployee(ddlElement.SelectedValue, ddlEmployee);
            BindRole(ddlElement.SelectedValue,ddlRole);
            getSubDepartment(ddlElement.SelectedValue, ddlSubDepartment);
        }
        protected void ddlSubDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlSubDepartment.SelectedValue != "")
            {
                if (ddlSubDepartment.SelectedValue == "Post-Sales")
                {
                    divGroup.Visible = true;
                }
                else
                {
                    divGroup.Visible = false;
                }
            }
            else
            {
                divGroup.Visible = false;
            }
        }
        void getSubDepartment(string Element , DropDownList ddl)
        {
            if (Element == "HR")
            {
                ddl.Items.Clear();
                ddl.Items.Add(new ListItem("Select Option", ""));
                ddl.Items.Add(new ListItem("Payroll", "Recruiter"));
                ddl.Items.Add(new ListItem("Recruiter", "Recruiter"));
            }
            if (Element == "EA")
            {
                ddl.Items.Clear();
                ddl.Items.Add(new ListItem("Select Option", ""));
                ddl.Items.Add(new ListItem("Pre-Sales", "Pre-Sales"));
                ddl.Items.Add(new ListItem("Post-Sales", "Post-Sales"));
                ddl.Items.Add(new ListItem("Internal", "Internal"));
            }
            if (Element == "Supervisor" || Element == "Coordinator")
            {
                ddl.Items.Clear();
                ddl.Items.Add(new ListItem("Select Option", ""));
                ddl.Items.Add(new ListItem("Post-Sales", "Post-Sales"));
            }
        }
        void BindRole(string Element , DropDownList ddl)
        {
            if (Element == "HR")
            {
                ddl.Items.Clear();
                ddl.Items.Add(new ListItem("Select Option", ""));
                ddl.Items.Add(new ListItem("Report To", "Report To"));
                ddl.SelectedValue = "Report To";
            }
            if (Element == "EA")
            {
                ddl.Items.Clear();
                ddl.Items.Add(new ListItem("Select Option", ""));
                ddl.Items.Add(new ListItem("CEM", "CEM"));
                ddl.Items.Add(new ListItem("HOD", "HOD"));
                ddl.Items.Add(new ListItem("Report To", "Report To"));
            }
            if (Element == "Supervisor" || Element == "Coordinator")
            {
                ddl.Items.Clear();
                ddl.Items.Add(new ListItem("Select Option", ""));
                ddl.Items.Add(new ListItem("CEM", "CEM"));
                ddl.SelectedValue = "CEM";
            }
        }
        void FillListView()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 78;
            PL.AutoId = ddlDirectorSearch.SelectedValue;
            PL.String1 = ddlElementSearch.SelectedValue;
            PL.String2 = ddlRoleSearch.SelectedValue;
            PL.EmpId = ddlEmployeeSearch.SelectedValue;
            PL.String3 = ddlSubDepartmentSearch.SelectedValue;
            PL.String4 = ddlGroupSearch.SelectedValue;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            LV.DataSource = dt;
            LV.DataBind();
        }
        void ClearField()
        {
            ddlDirector.SelectedIndex = -1;
            ddlElement.SelectedIndex = -1;
            ddlEmployee.SelectedIndex = -1;
            ddlSubDepartment.SelectedIndex = -1;
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
            EssPL PL = new EssPL();
            PL.OpCode = 78;
            PL.AutoId = id;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            if (dt.Rows.Count > 0)
            {
                BindEmployee(dt.Rows[0]["Element"].ToString(), ddlEmployee);
                getSubDepartment(dt.Rows[0]["Element"].ToString() , ddlSubDepartment);
                BindRole(dt.Rows[0]["Element"].ToString(), ddlRole);
                ddlDirector.SelectedIndex = ddlDirector.Items.IndexOf(ddlDirector.Items.FindByValue(dt.Rows[0]["DirectorId"].ToString()));
                ddlElement.SelectedIndex = ddlElement.Items.IndexOf(ddlElement.Items.FindByValue(dt.Rows[0]["Element"].ToString()));
                ddlRole.SelectedIndex = ddlRole.Items.IndexOf(ddlRole.Items.FindByValue(dt.Rows[0]["EARole"].ToString()));
                ddlGroup.SelectedIndex = ddlGroup.Items.IndexOf(ddlGroup.Items.FindByValue(dt.Rows[0]["PSGroup"].ToString()));
                ddlEmployee.SelectedIndex = ddlEmployee.Items.IndexOf(ddlEmployee.Items.FindByValue(dt.Rows[0]["EmpId"].ToString()));
                ddlSubDepartment.SelectedIndex = ddlSubDepartment.Items.IndexOf(ddlSubDepartment.Items.FindByValue(dt.Rows[0]["SubDepartment"].ToString()));
                if (dt.Rows[0]["SubDepartment"].ToString() == "Post-Sales")
                {
                    divGroup.Visible = true;
                }
                else
                {
                    divGroup.Visible = false;
                }
                if (PL.dt.Rows[0]["IsActive"].ToString() == "False")
                {
                    chkActive.Checked = false;
                }
                else
                {
                    chkActive.Checked = true;
                }
                ViewState["Mode"] = "Edit";
                divView.Visible = false;
                divAddEdit.Visible = true;
            }
            else
            {
                ClearField();
            }
        }
        string GetParentServiceXml()
        {
            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<DirectorId><![CDATA[" + ddlDirector.SelectedValue + "]]></DirectorId>";
            xml += "<Element><![CDATA[" + ddlElement.SelectedValue + "]]></Element>";
            xml += "<EmpId><![CDATA[" + ddlEmployee.SelectedValue + "]]></EmpId>";
            xml += "<SubDepartment><![CDATA[" + ddlSubDepartment.SelectedValue + "]]></SubDepartment>";
            xml += "<EARole><![CDATA[" + ddlRole.SelectedValue + "]]></EARole>";
            xml += "<PSGroup><![CDATA[" + ddlGroup.SelectedValue + "]]></PSGroup>";
            xml += "<IsActive><![CDATA[" + chkActive.Checked + "]]></IsActive>";
            xml += "</tr>";
            xml += "</tbl>";
            return xml;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();
            if (ViewState["Mode"].ToString() == "Add")
            {
                PL.OpCode = 79;
                PL.XML = GetParentServiceXml();
                PL.CreatedBy = Session["UserAutoId"].ToString();
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
                PL.OpCode = 80;
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
        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void ddlSubDepartmentSearch_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (ddlSubDepartmentSearch.SelectedValue != "")
            {
                if (ddlSubDepartmentSearch.SelectedValue == "Post-Sales")
                {
                    divGroupSearch.Visible = true;
                }
                else
                {
                    divGroupSearch.Visible = false;
                }
            }
            else
            {
                divGroupSearch.Visible = false;
            }
        } 
        protected void ddlElementSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindEmployee(ddlElementSearch.SelectedValue, ddlEmployeeSearch); 
            BindRole(ddlElementSearch.SelectedValue, ddlRoleSearch);
            getSubDepartment(ddlElementSearch.SelectedValue , ddlSubDepartmentSearch);

        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
    }
}