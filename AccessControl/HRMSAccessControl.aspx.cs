using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.AccessControl
{
    public partial class HRMSAccessControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillEmpAccess();
            }
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
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            divView.Visible = false;
            divEdit.Visible = true;
            FillActiveEmployee();
            divEmployeeDetails.Visible = false;
            divEmployeeAccess.Visible = false;
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
                    SetForEdit(int.Parse(chkSelect.Attributes["Autoid"].ToString()));
                    divView.Visible = false;
                    divEdit.Visible = true;

                    break;
                }
            }
        }
        void SetForEdit(int empid)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 27;
            PL.EmpId = empid;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                FillActiveEmployee();
                GetEmployeeDepartment(Convert.ToInt32(PL.dt.Rows[0]["EmpId"].ToString()));
                ddlEmployeeName.SelectedIndex = ddlEmployeeName.Items.IndexOf(ddlEmployeeName.Items.FindByValue(PL.dt.Rows[0]["EmpId"].ToString()));
                divEmployeeDetails.Visible = true;
                divEmployeeAccess.Visible = true;

                string designation = getDesignationId(Convert.ToInt32(PL.dt.Rows[0]["EmpId"].ToString()));
                FillListView(designation, PL.dt.Rows[0]["EmpId"].ToString());
                foreach (DataRow dr in PL.dt.Rows)
                {
                    foreach (ListViewItem lvItem in LV_Access_Menu_Company.Items)
                    {
                        HiddenField hidAutoId = (HiddenField)lvItem.FindControl("hidautoid");
                        if (dr[0].ToString() == hidAutoId.Value.ToString())
                        {
                            if (!string.IsNullOrEmpty(dr["RegionId"].ToString()))
                            {
                                CheckBoxList chkaction = (CheckBoxList)lvItem.FindControl("chkactionRegion");
                                string[] action = dr["RegionId"].ToString().Split('^');
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
                            if (!string.IsNullOrEmpty(dr["IndustryId"].ToString()))
                            {
                                CheckBoxList chkaction = (CheckBoxList)lvItem.FindControl("chkactionIndustry");
                                string[] action = dr["IndustryId"].ToString().Split('^');
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
                            if (!string.IsNullOrEmpty(dr["Action"].ToString()))
                            {
                                CheckBoxList chkaction = (CheckBoxList)lvItem.FindControl("chkactionMenu");
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
        void FillEmpAccess()
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 25;
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
        void FillListView(string designation, string empid)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.OpCode = 24;
            PL.Designation = designation;
            PL.Type = 2;
            PL.EmpId = empid;
            MenuAccessDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Access_Menu_Company.DataSource = PL.dt;
                LV_Access_Menu_Company.DataBind();
            }
            else
            {
                LV_Access_Menu_Company.DataSource = PL.dt;
                LV_Access_Menu_Company.DataBind();
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
        public DataTable GetRegionAction(string linkid)
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 19;
            PL.AutoId = linkid;
            MenuDL.returnTable(PL);
            return PL.dt;
        }
        public DataTable GetIndustryAction(string linkid)
        {
            MenuPL PL = new MenuPL();
            PL.OpCode = 20;
            PL.RegionId = linkid;
            MenuDL.returnTable(PL);
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
                divEmployeeDetails.Visible = true;
                divEmployeeAccess.Visible = true;
                GetEmployeeDepartment(Convert.ToInt32(ddlEmployeeName.SelectedValue));
                string designation = getDesignationId(Convert.ToInt32(ddlEmployeeName.SelectedValue));
                FillListView(designation, ddlEmployeeName.SelectedValue);
                SetForEdit(Convert.ToInt32(ddlEmployeeName.SelectedValue));
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MenuAccessPL PL = new MenuAccessPL();
            PL.EmpId = ddlEmployeeName.SelectedValue;
            PL.CreatedBy = Session["UserAutoId"].ToString();
            string xml = GetXmlData();
            if (xml.Contains("MenuId"))
            {
                PL.OpCode = 26;
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Parental Menu should be select');", true);
            }
        }
        string GetXmlData()
        {
            string XML = "<tbl>";
            foreach (ListViewItem lvItem in LV_Access_Menu_Company.Items)
            {
                string Region = "";
                string Industry = "";
                string Action = "";
                HiddenField hidAutoId = (HiddenField)lvItem.FindControl("hidautoid");
                CheckBoxList chkactionRegion = (CheckBoxList)lvItem.FindControl("chkactionRegion");
                CheckBoxList chkactionIndustry = (CheckBoxList)lvItem.FindControl("chkactionIndustry");
                CheckBoxList chkactionMenu = (CheckBoxList)lvItem.FindControl("chkactionMenu");
                foreach (ListItem reg in chkactionRegion.Items)
                {
                    if (reg.Selected)
                    {
                        Region += reg.Value + "^";
                    }
                }
                foreach (ListItem ind in chkactionIndustry.Items)
                {
                    if (ind.Selected)
                    {
                        Industry += ind.Value + "^";
                    }
                }
                foreach (ListItem act in chkactionMenu.Items)
                {
                    if (act.Selected)
                    {
                        Action += act.Value + "^";
                    }
                }
                XML += "<tr>";
                XML += "<EmpId>" + ddlEmployeeName.SelectedValue + "</EmpId>";
                XML += "<RegionId>" + Region + "</RegionId>";
                XML += "<IndustryId>" + Industry + "</IndustryId>";
                XML += "<Action>" + Action + "</Action>";
                XML += "<MenuId>" + hidAutoId.Value + "</MenuId>";
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
        protected void LV_Hid_Region_Industry_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Panel pnlRegion = (Panel)e.Item.FindControl("pnlRegion");
            Panel pnlIndustry = (Panel)e.Item.FindControl("pnlIndustry");
            HiddenField hdnMastermenu = (HiddenField)e.Item.FindControl("hidIsParentMenu");
            if(hdnMastermenu.Value == "True")
            {
                pnlRegion.Visible = true;
                pnlIndustry.Visible = true;
            }
            else
            {
                pnlRegion.Visible = false;
                pnlIndustry.Visible = false;
            }
        }
    }
}