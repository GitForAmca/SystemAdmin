using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Adapters;
using SystemAdmin.App_Code;

namespace SystemAdmin.AccessControl
{
    public partial class OrganizationalAccessControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillActiveFilterEmployee();
                //FillEmpAccess(); 
                getDepartmentFilter();
            }
        }
        void FillActiveFilterEmployee()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 19;
            DropdownDL.returnTable(PL);
            ddlEmployeeFilter.DataSource = PL.dt;
            ddlEmployeeFilter.DataValueField = "empid";
            ddlEmployeeFilter.DataTextField = "EmpName";
            ddlEmployeeFilter.DataBind();
            ddlEmployeeFilter.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void FillActiveEmployee()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 14;
            DropdownDL.returnTable(PL);
            ddlEmployeeName.DataSource = PL.dt;
            ddlEmployeeName.DataValueField = "Autoid";
            ddlEmployeeName.DataTextField = "Name";
            ddlEmployeeName.DataBind();
            ddlEmployeeName.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void FillGroup(int empid)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 20;
            PL.AutoId = empid;
            MenuAccessDL.returnTable(PL);
            ddlUpdateGroupCompany.DataSource = PL.dt;
            ddlUpdateGroupCompany.DataValueField = "CompanyId";
            ddlUpdateGroupCompany.DataTextField = "Name";
            ddlUpdateGroupCompany.DataBind();
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            divView.Visible = false;
            divEdit.Visible = true;
            FillActiveEmployee();
            divEmployeeDetails.Visible = false;
            divEmployeeAccess.Visible = false;
            ddlEmployeeName.Enabled = true;
            divUpdateGroup.Visible = false;
            btnSave.Visible = true;
            btnUpdateAccess.Visible = false;
            divAddGroup.Visible = false;
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_Employee_Access.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                    hidAutoidMain.Value = chkSelect.Attributes["Autoid"];
                    SetForEdit(chkSelect.Attributes["Autoid"].ToString(), chkSelect.Attributes["GroupId"].ToString());
                    ddlEmployeeName.Enabled = false;
                    divView.Visible = false;
                    divEdit.Visible = true;
                    divUpdateGroup.Visible = true;
                    divAddGroup.Visible = false;
                    btnSave.Visible = false;
                    btnUpdateAccess.Visible = true;
                    break;
                }
            }
        }
        void SetForEdit(string empid, string groupid)
        {
            string designation = getDesignationId(Convert.ToInt32(empid.ToString()));
            FillListView(designation, empid.ToString(), groupid);
            /////////////////////////////////////
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 23;
            PL.EmpId = empid;
            PL.Industry = groupid;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                FillActiveEmployee();
                FillGroup(Convert.ToInt32(PL.dt.Rows[0]["EmpId"].ToString()));
                GetEmployeeDepartment(Convert.ToInt32(PL.dt.Rows[0]["EmpId"].ToString()));
                ddlEmployeeName.SelectedIndex = ddlEmployeeName.Items.IndexOf(ddlEmployeeName.Items.FindByValue(PL.dt.Rows[0]["EmpId"].ToString()));
                ddlUpdateGroupCompany.SelectedIndex = ddlUpdateGroupCompany.Items.IndexOf(ddlUpdateGroupCompany.Items.FindByValue(groupid.ToString()));
                divEmployeeDetails.Visible = true;
                divEmployeeAccess.Visible = true;

                getSelectedGroup(PL.dt.Rows[0]["EmpId"].ToString());
                foreach (DataRow dr in PL.dt.Rows)
                {
                    foreach (ListViewItem lvItem in LV_Access_Menu_Company.Items)
                    {
                        HiddenField hidAutoId = (HiddenField)lvItem.FindControl("hidautoid");
                        if (dr[0].ToString() == hidAutoId.Value.ToString())
                        {
                            CheckBox chkIsFullAccess = (CheckBox)lvItem.FindControl("chkIsFullAccess");
                            if (dr["FullAccess"].ToString() == "True")
                            {
                                chkIsFullAccess.Checked = true;
                            }
                            if (!string.IsNullOrEmpty(dr["Action"].ToString()))
                            {
                                CheckBoxList chkaction = (CheckBoxList)lvItem.FindControl("chkaction");
                                string[] action = dr["Action"].ToString().Split('^');
                                foreach (string str in action)
                                {
                                    foreach (ListItem li in chkaction.Items)
                                    {
                                        if (li.Value == str)
                                        {
                                            li.Selected = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (ListViewItem lvItem in LV_Access_Menu_Company.Items)
                {
                    CheckBoxList chkaction = (CheckBoxList)lvItem.FindControl("chkaction");
                    foreach (ListItem li in chkaction.Items)
                    {
                        li.Selected = false;
                        break;
                    }
                }
            }
        }
        protected void ddlUpdateGroupCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetForEdit(hidAutoidMain.Value, ddlUpdateGroupCompany.SelectedValue);
        }
        void getSelectedGroup(string empid)
        {
            BindCheckBoxList();
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 30;
            PL.EmpId = empid;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            foreach (ListItem gr in chkGroupCompany.Items)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["GroupId"].ToString() == gr.Value)
                    {
                        gr.Selected = true;
                        break;
                    }
                }
            }
        }
        void FillEmpAccess()
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 22;
            PL.AutoId = ddlEmployeeFilter.SelectedValue;
            PL.Department = ddlDepartmentFilter.SelectedValue;
            PL.SubDepartment = ddlSubDepartmentFilter.SelectedValue;
            PL.Designation = ddlDesignationFilter.SelectedValue;
            PL.AutoId = ddlEmployeeFilter.SelectedValue;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Employee_Access.DataSource = PL.dt;
                LV_Employee_Access.DataBind();
            }
            else
            {
                LV_Employee_Access.DataSource = PL.dt;
                LV_Employee_Access.DataBind();
            }
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            if (ddlEmployeeFilter.SelectedValue != "" || ddlDepartmentFilter.SelectedValue != "" || ddlSubDepartmentFilter.SelectedValue != "" || ddlDesignationFilter.SelectedValue != "")
            {
                FillEmpAccess();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowError('Kindly Select any filter');", true);
            }
        }
        void FillListView(string designation, string empid, string Group)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 24;
            PL.Designation = designation;
            PL.Type = 1;
            PL.EmpId = empid;
            PL.Industry = Group;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Access_Menu_Company.DataSource = PL.dt;
                LV_Access_Menu_Company.DataBind();
                LV_Access_Menu_Company_Update.DataSource = PL.dt;
                LV_Access_Menu_Company_Update.DataBind();
            }
            else
            {
                LV_Access_Menu_Company.DataSource = "";
                LV_Access_Menu_Company.DataBind();
                LV_Access_Menu_Company_Update.DataSource = "";
                LV_Access_Menu_Company_Update.DataBind();
            }
        }
        void GetEmployeeDepartment(int id)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 15;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Employee_Menu_Details.DataSource = PL.dt;
                LV_Employee_Menu_Details.DataBind();
            }
            else
            {
                LV_Employee_Menu_Details.DataSource = PL.dt;
                LV_Employee_Menu_Details.DataBind();
            }
        }
        public string GetInt(string a)
        {
            return "clschkaction" + a;
        }
        public DataTable GetAction(string linkid)
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 18;
            PL.AutoId = linkid;
            MenuDL.returnTable(PL);
            return PL.dt;
        }
        private void BindCheckBoxList()
        {
            chkGroupCompany.DataSource = GetGroup();
            chkGroupCompany.DataBind();
        }
        protected void chkGroupCompany_DataBinding(object sender, EventArgs e)
        {
            ((CheckBoxList)sender).DataSource = GetGroup();
        }
        public DataTable GetGroup()
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 20;
            PL.AutoId = ddlEmployeeName.SelectedValue;
            MenuAccessDL.returnTable(PL);
            return PL.dt;
        }
        public string getDesignationId(int id)
        {
            string msg = "";
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 16;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                msg = PL.dt.Rows[0]["Autoids"].ToString();
            }
            else
            {
                msg = "";
            }
            return msg;
        }
        protected void ddlEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmployeeName.SelectedValue != "")
            {
                MenuAccessPL PL = new MenuAccessPL();
                PL.OpCode = 23;
                PL.EmpId = ddlEmployeeName.SelectedValue;
                PL.Industry = hidGroupIdMain.Value;
                MenuAccessDL.returnTable(PL);
                DataTable dt = PL.dt;
                if (PL.dt.Rows.Count > 0)
                {
                    divAddGroup.Visible = false;
                    divEmployeeDetails.Visible = false;
                    LV_Access_Menu_Company.DataSource = "";
                    LV_Access_Menu_Company.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Access has already been granted. If you want to update it, please use the update action button on the dashboard.');", true);
                }
                else
                {
                    divAddGroup.Visible = true;
                    divEmployeeDetails.Visible = true;
                    divEmployeeAccess.Visible = true;
                    GetEmployeeDepartment(Convert.ToInt32(ddlEmployeeName.SelectedValue));
                    BindCheckBoxList();
                }
            }
        }
        protected void chkGroupCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            var checkedValues = chkGroupCompany.Items
                                                .Cast<ListItem>()
                                                .Where(item => item.Selected)
                                                .Select(item => item.Value);
            string selectedGroup = string.Join(",", checkedValues);
            string designation = getDesignationId(Convert.ToInt32(ddlEmployeeName.SelectedValue));
            FillListView(designation, ddlEmployeeName.SelectedValue, selectedGroup);
        }
        protected void btnViewAction_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var item = (ListViewItem)btn.NamingContainer;

            int id = int.Parse(((HiddenField)item.FindControl("hidEmpid")).Value);
            hidEmpidMain.Value = ((HiddenField)item.FindControl("hidEmpid")).Value;
            hidGroupIdMain.Value = ((HiddenField)item.FindControl("hidGroupId")).Value;
            SetForEditGroupWise(id);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openpp", "OpenPopUpAction();", true);
        }
        void SetForEditGroupWise(int empid)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 23;
            PL.EmpId = empid;
            PL.Industry = hidGroupIdMain.Value;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                string designation = getDesignationId(Convert.ToInt32(PL.dt.Rows[0]["EmpId"].ToString()));
                FillListView(designation, PL.dt.Rows[0]["EmpId"].ToString(), hidGroupIdMain.Value);
                foreach (DataRow dr in PL.dt.Rows)
                {
                    foreach (ListViewItem lvItem in LV_Access_Menu_Company_Update.Items)
                    {
                        HiddenField hidAutoId = (HiddenField)lvItem.FindControl("hidautoidUpdate");
                        if (dr[0].ToString() == hidAutoId.Value.ToString())
                        {
                            CheckBox chkIsFullAccess = (CheckBox)lvItem.FindControl("chkIsFullAccess");
                            if (dr["FullAccess"].ToString() == "True")
                            {
                                chkIsFullAccess.Checked = true;
                            }
                            if (!string.IsNullOrEmpty(dr["Action"].ToString()))
                            {
                                CheckBoxList chkaction = (CheckBoxList)lvItem.FindControl("chkactionUpdate");
                                string[] action = dr["Action"].ToString().Split('^');
                                foreach (string str in action)
                                {
                                    foreach (ListItem li in chkaction.Items)
                                    {
                                        if (li.Value == str)
                                        {
                                            li.Selected = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
        private bool IsAnyItemChecked()
        {
            foreach (ListItem item in chkGroupCompany.Items)
            {
                if (item.Selected)
                {
                    return true;
                }
            }
            return false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsAnyItemChecked())
            {
                MenuAccessPL PL = new MenuAccessPL();
                PL.EmpId = ddlEmployeeName.SelectedValue;
                PL.CreatedBy = Session["UserAutoId"].ToString();
                string xml = GetXmlData();
                if (xml.Contains("MenuId"))
                {
                    PL.OpCode = 21;
                    PL.XML = xml;
                    PL.EmpId = ddlEmployeeName.SelectedValue;
                    MenuAccessDL.returnTable(PL);
                    if (!PL.isException)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Save successfully.');", true);
                        divEdit.Visible = false;
                        divView.Visible = true;
                        FillEmpAccess();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Action should be select');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Select atleast one Company');", true);
            }
        }
        string GetXmlData()
        {
            string XML = "<tbl>";
            foreach (ListItem gr in chkGroupCompany.Items)
            {
                if (gr.Selected)
                {
                    foreach (ListViewItem lvItem in LV_Access_Menu_Company.Items)
                    {
                        CheckBox chkSelectFullAccess = (CheckBox)lvItem.FindControl("chkIsFullAccess");
                        string actionid1 = "";
                        HiddenField hidAutoId = (HiddenField)lvItem.FindControl("hidautoid");
                        CheckBoxList chkaction1 = (CheckBoxList)lvItem.FindControl("chkaction");
                        foreach (ListItem li in chkaction1.Items)
                        {
                            if (li.Selected)
                            {
                                actionid1 += li.Value + "^";
                            }
                        }
                        XML += "<tr>";
                        XML += "<EmpId>" + ddlEmployeeName.SelectedValue + "</EmpId>";
                        XML += "<MenuId>" + hidAutoId.Value + "</MenuId>";
                        XML += "<GroupId>" + gr.Value + "</GroupId>";
                        XML += "<Action>" + actionid1 + "</Action>";
                        XML += "<FullAccess>" + (chkSelectFullAccess.Checked == true ? 1 : 0) + "</FullAccess>";
                        XML += "</tr>";
                    }
                }
            }
            XML += "</tbl>";
            return XML;
        }
        protected void btnUpdateAction_Click(object sender, EventArgs e)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.EmpId = hidEmpidMain.Value;
            PL.CreatedBy = Session["UserAutoId"].ToString();
            string xml = GetXmlDataForUpdate();
            PL.OpCode = 31;
            PL.XML = xml;
            PL.Industry = hidGroupIdMain.Value;
            MenuAccessDL.returnTable(PL);
            if (!PL.isException)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Update successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        protected void btnUpdateAccess_Click(object sender, EventArgs e)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.EmpId = hidAutoidMain.Value;
            PL.Industry = ddlUpdateGroupCompany.SelectedValue;
            PL.CreatedBy = Session["UserAutoId"].ToString();
            string xml = GetXmlDataForUpdateInside(hidAutoidMain.Value, ddlUpdateGroupCompany.SelectedValue);
            PL.OpCode = 31;
            PL.XML = xml;
            MenuAccessDL.returnTable(PL);
            if (!PL.isException)
            {
                divEdit.Visible = false;
                divView.Visible = true;
                FillEmpAccess();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowDone('Update successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
        string GetXmlDataForUpdateInside(string empid, string groupid)
        {
            string XML = "<tbl>";
            foreach (ListViewItem lvItem in LV_Access_Menu_Company.Items)
            {
                CheckBox chkSelectFullAccess = (CheckBox)lvItem.FindControl("chkIsFullAccess");
                string actionid1 = "";
                HiddenField hidAutoId = (HiddenField)lvItem.FindControl("hidautoid");
                CheckBoxList chkaction1 = (CheckBoxList)lvItem.FindControl("chkaction");
                foreach (ListItem li in chkaction1.Items)
                {
                    if (li.Selected)
                    {
                        actionid1 += li.Value + "^";
                    }
                }
                XML += "<tr>";
                XML += "<EmpId>" + empid + "</EmpId>";
                XML += "<MenuId>" + hidAutoId.Value + "</MenuId>";
                XML += "<GroupId>" + groupid + "</GroupId>";
                XML += "<Action>" + actionid1 + "</Action>";
                XML += "<FullAccess>" + (chkSelectFullAccess.Checked == true ? 1 : 0) + "</FullAccess>";
                XML += "</tr>";
            }
            XML += "</tbl>";
            return XML;
        }
        string GetXmlDataForUpdate()
        {
            string XML = "<tbl>";
            foreach (ListViewItem lvItem in LV_Access_Menu_Company_Update.Items)
            {
                CheckBox chkSelectFullAccess = (CheckBox)lvItem.FindControl("chkIsFullAccess");
                string actionid1 = "";
                HiddenField hidAutoId = (HiddenField)lvItem.FindControl("hidautoidUpdate");
                CheckBoxList chkaction1 = (CheckBoxList)lvItem.FindControl("chkactionUpdate");
                foreach (ListItem li in chkaction1.Items)
                {
                    if (li.Selected)
                    {
                        actionid1 += li.Value + "^";
                    }
                }
                XML += "<tr>";
                XML += "<EmpId>" + hidEmpidMain.Value + "</EmpId>";
                XML += "<MenuId>" + hidAutoId.Value + "</MenuId>";
                XML += "<GroupId>" + hidGroupIdMain.Value + "</GroupId>";
                XML += "<Action>" + actionid1 + "</Action>";
                XML += "<FullAccess>" + (chkSelectFullAccess.Checked == true ? 1 : 0) + "</FullAccess>";
                XML += "</tr>";
            }
            XML += "</tbl>";
            return XML;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divEdit.Visible = false;
            divView.Visible = true;
        }
        protected void ddldepartmentFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDepartmentFilter.SelectedValue != "")
            {
                getSubDepartmentFilter(Convert.ToInt32(ddlDepartmentFilter.SelectedValue));
                ddlDesignationFilter.DataSource = "";
                ddlDesignationFilter.DataBind();
            }
            else
            {
                ddlSubDepartmentFilter.DataSource = "";
                ddlSubDepartmentFilter.DataBind();

                ddlDesignationFilter.DataSource = "";
                ddlDesignationFilter.DataBind();
            }
        }
        void getDepartmentFilter()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 72; 
            DropdownDL.returnTable(PL);
            ddlDepartmentFilter.DataSource = PL.dt;
            ddlDepartmentFilter.DataValueField = "Id";
            ddlDepartmentFilter.DataTextField = "Name";
            ddlDepartmentFilter.DataBind();
            ddlDepartmentFilter.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getSubDepartmentFilter(int id)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 18;
            PL.AutoId = id;
            DropdownDL.returnTable(PL);
            ddlSubDepartmentFilter.DataSource = PL.dt;
            ddlSubDepartmentFilter.DataValueField = "Autoid";
            ddlSubDepartmentFilter.DataTextField = "GroupName";
            ddlSubDepartmentFilter.DataBind();
            ddlSubDepartmentFilter.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getDesignationFilter()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 73;
            PL.AutoId = ddlSubDepartmentFilter.SelectedValue;
            DropdownDL.returnTable(PL);
            ddlDesignationFilter.DataSource = PL.dt;
            ddlDesignationFilter.DataValueField = "Id";
            ddlDesignationFilter.DataTextField = "Name";
            ddlDesignationFilter.DataBind();
            ddlDesignationFilter.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        protected void ddlSubDepartmentFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubDepartmentFilter.SelectedValue != "")
            {
                getDesignationFilter();
            }
            else
            {
                ddlDesignationFilter.DataSource = "";
                ddlDesignationFilter.DataBind();
            }
        }
    }
}