using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.AccessControl
{
    public partial class DataAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillGroup(ddlGroupFilter);
                FillListView(ddlGroupFilter.SelectedValue);
                FillEmployee(ddlEmpName);
                BindElement();
            }
            if (!IsPostBack && Session["SuccessMessage"] != null)
            {
                string script = Session["SuccessMessage"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", script, true);
                Session.Remove("SuccessMessage");
            }
        }
        void FillGroup(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 65;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "GroupId";
            ddl.DataTextField = "Name";
            ddl.DataBind();
        }
        void FillEmployee(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 4;
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
            PL.ServiceTypeAutoid = "1,2,3,4,5";
            DropdownDL.returnTable(PL);
            ddlElement.DataSource = PL.dt;
            ddlElement.DataValueField = "Element";
            ddlElement.DataTextField = "Element";
            ddlElement.DataBind();
            ddlElement.Items.Insert(0, new ListItem("Select an option", ""));
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
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 3;
            PL.AutoId = 1;
            DropdownDL.returnTable(PL);
            return PL.dt;
        }
        void getDDLGroup()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 3;
            PL.AutoId = 1;
            DropdownDL.returnTable(PL);
            ddlUpdateGroupCompany.DataSource = PL.dt;
            ddlUpdateGroupCompany.DataValueField = "Autoid";
            ddlUpdateGroupCompany.DataTextField = "Name";
            ddlUpdateGroupCompany.DataBind();
        }
        void getAccessEmp(string Element, string CEM)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 66;
            PL.RegionId = Element;
            PL.AutoId = CEM;
            DropdownDL.returnTable(PL);
            ddlAccessName.DataSource = PL.dt;
            ddlAccessName.DataValueField = "Autoid";
            ddlAccessName.DataTextField = "Name";
            ddlAccessName.DataBind();
            ddlAccessName.Items.Insert(0, new ListItem("Select an option", ""));
        }
        void getCEMList()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 68;
            DropdownDL.returnTable(PL);
            ddlAccessName.DataSource = PL.dt;
            ddlAccessName.DataValueField = "Autoid";
            ddlAccessName.DataTextField = "Name";
            ddlAccessName.DataBind();
            ddlAccessName.Items.Insert(0, new ListItem("Select an option", ""));
        }
        public DataTable getCEM(string EmpId)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 67;
            PL.AutoId = EmpId;
            DropdownDL.returnTable(PL);
            return PL.dt;
        }
        void FillListView(string GroupId)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 66;
            PL.GroupId = GroupId;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Portal_Access_Mapping.DataSource = PL.dt;
                LV_Portal_Access_Mapping.DataBind();
            }
            else
            {
                LV_Portal_Access_Mapping.DataSource = "";
                LV_Portal_Access_Mapping.DataBind();
            }
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView(ddlGroupFilter.SelectedValue);
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            divView.Visible = false;
            divAddEdit.Visible = true;
            divAddGroup.Visible = true;
            divUpdateGroup.Visible = false;
            ddlUpdateGroupCompany.Enabled = true;
            ddlEmpName.Enabled = true;
            ddlElement.Enabled = true;
            BindCheckBoxList();
            clearField();
            ViewState["Mode"] = "Add";
        }
        protected void ddlElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlElement.SelectedValue != "")
            {
                foreach (ListItem item in chkGroupCompany.Items)
                {
                    if (item.Selected)
                    {
                        if (IsEntryExist(ddlEmpName.SelectedValue, ddlElement.SelectedValue, item.Value, ddlAccessName.SelectedValue) == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Exist Already!');", true);
                            ddlElement.SelectedIndex = -1;
                            ddlAccessName.Items.Clear();
                        }
                        else
                        {
                            if(ddlElement.SelectedValue == "CEM")
                            {
                                getCEMList();
                            }
                            else
                            {
                                getAccessEmp(ddlElement.SelectedValue, hidCEM.Value);
                            }
                        }
                    }
                }
            }
        }
        bool IsEntryExist(string EmpName, string Element, string GroupId, string AccessEmpId)
        {
            bool IsGiven;
            EssPL PL = new EssPL();
            PL.OpCode = 67;
            PL.String1 = Element;
            PL.String2 = EmpName;
            PL.String3 = GroupId;
            PL.String4 = AccessEmpId;
            EssDL.returnTable(PL);
            if (PL.dt.Rows[0]["countData"].ToString() != "0")
            {
                IsGiven = true;
            }
            else
            {
                IsGiven = false;
            }
            return IsGiven;
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
        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmpName.SelectedValue != "")
            {
                if (IsAnyItemChecked())
                {
                    DataTable cemdt = getCEM(ddlEmpName.SelectedValue);
                    if (cemdt.Rows.Count > 0)
                    {
                        hidCEM.Value = cemdt.Rows[0]["Autoid"].ToString();
                        foreach (ListItem item in chkGroupCompany.Items)
                        {
                            if (item.Selected)
                            {
                                if (IsEntryExist(ddlEmpName.SelectedValue, ddlElement.SelectedValue, item.Value, ddlAccessName.SelectedValue) == true)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Exist Already!');", true);
                                    divAddGroup.Visible = false;
                                    divEmployeeDetails.Visible = false;
                                    LV_Access_Menu_Company.DataSource = "";
                                    LV_Access_Menu_Company.DataBind();
                                }
                                else
                                {
                                    divAddGroup.Visible = true;
                                    divEmployeeDetails.Visible = true;
                                    divEmployeeAccess.Visible = true;
                                    GetEmployeeDepartment(Convert.ToInt32(ddlEmpName.SelectedValue));
                                }
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Please update the CEM first');", true);
                        ddlEmpName.SelectedIndex = -1;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Select atleast one Company');", true);
                    ddlEmpName.SelectedIndex = -1;
                }
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
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_Portal_Access_Mapping.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        string EmpId = chkSelect.Attributes["EmpId"];
                        string ElementName = chkSelect.Attributes["ElementName"];
                        string AccessEmpId = chkSelect.Attributes["AccessEmpId"];
                        string GroupId = chkSelect.Attributes["GroupId"];
                        //-----------------
                        EssPL PL = new EssPL();
                        PL.OpCode = 66;
                        PL.AutoId = EmpId;
                        PL.String1 = ElementName;
                        PL.String2 = AccessEmpId;
                        PL.GroupId = GroupId;
                        EssDL.returnTable(PL);
                        DataTable dt = PL.dt;
                        //--------------------------------
                        if (dt.Rows.Count > 0)
                        {
                            getDDLGroup();
                            ddlUpdateGroupCompany.SelectedIndex = ddlUpdateGroupCompany.Items.IndexOf(ddlUpdateGroupCompany.Items.FindByValue(dt.Rows[0]["GroupId"].ToString()));
                            ddlEmpName.Enabled = false;
                            ddlEmpName.SelectedIndex = ddlEmpName.Items.IndexOf(ddlEmpName.Items.FindByValue(dt.Rows[0]["EmpId"].ToString()));
                            GetEmployeeDepartment(Convert.ToInt32(dt.Rows[0]["EmpId"].ToString()));
                            ddlElement.Enabled = false;
                            ddlElement.SelectedIndex = ddlElement.Items.IndexOf(ddlElement.Items.FindByValue(dt.Rows[0]["ElementName"].ToString()));
                            ddlAccessName.Enabled = false;

                            if (dt.Rows[0]["ElementName"].ToString() == "CEM")
                            {
                                getCEMList();
                                ddlAccessName.SelectedIndex = ddlAccessName.Items.IndexOf(ddlAccessName.Items.FindByValue(dt.Rows[0]["AccessEmpId"].ToString()));
                            }
                            else
                            {
                                getAccessEmp(dt.Rows[0]["ElementName"].ToString(), dt.Rows[0]["ClientEngagementManager"].ToString());
                                ddlAccessName.SelectedIndex = ddlAccessName.Items.IndexOf(ddlAccessName.Items.FindByValue(dt.Rows[0]["AccessEmpId"].ToString()));
                            }


                            txtEndDate.Text = dt.Rows[0]["EndDate"].ToString();
                            if (PL.dt.Rows[0]["IsActive"].ToString() == "False")
                            {
                                chkActive.Checked = false;
                            }
                            else
                            {
                                chkActive.Checked = true;
                            }
                            getAccessMenuList(dt.Rows[0]["EmpId"].ToString(), LV_Access_Menu_Company);
                            ////////////////////////
                            PL.OpCode = 69;
                            PL.AutoId = EmpId;
                            PL.String1 = ElementName;
                            PL.String2 = AccessEmpId;
                            PL.GroupId = GroupId;
                            EssDL.returnTable(PL);
                            DataTable dt2 = PL.dt;
                            if (dt2.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt2.Rows)
                                {
                                    foreach (ListViewItem item2 in LV_Access_Menu_Company.Items)
                                    {
                                        HiddenField hdnMenuid = (HiddenField)item2.FindControl("hidautoid");
                                        if (hdnMenuid.Value == row["MenuId"].ToString())
                                        {
                                            CheckBox chkIsChecked = (CheckBox)item2.FindControl("chkIsChecked");
                                            if (row["MenuAccess"].ToString() == "True")
                                            {
                                                chkIsChecked.Checked = true;
                                            }
                                            else
                                            {
                                                chkIsChecked.Checked = false;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        ViewState["Mode"] = "Edit";
                        divView.Visible = false;
                        divAddEdit.Visible = true;
                        divAddGroup.Visible = false;
                        divUpdateGroup.Visible = true;
                        ddlUpdateGroupCompany.Enabled = false;
                        break;
                    }
                }
            }
        }
        void clearField()
        {
            ddlEmpName.SelectedIndex = -1;
            ddlElement.SelectedIndex = -1;
            ddlAccessName.SelectedIndex = -1;
            txtEndDate.Text = string.Empty;
        }
        string XMLField(string GroupId)
        {
            string xml = "";
            foreach (ListViewItem item in LV_Access_Menu_Company.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkIsChecked");
                if (chkSelect != null)
                {
                    CheckBox chkIsChecked = (CheckBox)item.FindControl("chkIsChecked");
                    HiddenField hdnMenuId = (HiddenField)item.FindControl("hidautoid");
                    xml += "<tr>";
                    xml += "<EmpId><![CDATA[" + ddlEmpName.SelectedValue + "]]></EmpId>";
                    xml += "<ElementName><![CDATA[" + ddlElement.SelectedValue + "]]></ElementName>";
                    xml += "<AccessEmpId><![CDATA[" + ddlAccessName.SelectedValue + "]]></AccessEmpId>";
                    xml += "<EndDate><![CDATA[" + txtEndDate.Text.Trim() + "]]></EndDate>";
                    xml += "<IsActive><![CDATA[" + chkActive.Checked + "]]></IsActive>";
                    xml += "<GroupId><![CDATA[" + GroupId + "]]></GroupId>";
                    xml += "<MenuId><![CDATA[" + hdnMenuId.Value + "]]></MenuId>";
                    xml += "<MenuAccess><![CDATA[" + (chkIsChecked.Checked == true ? 1 : 0) + "]]></MenuAccess>"; 
                    xml += "</tr>";
                }
            }
            return xml;
        }
        string GetChildStatusXml()
        {
            string xml = "<tbl>";
            foreach (ListItem gr in chkGroupCompany.Items)
            {
                if (gr.Selected)
                {
                    xml += XMLField(gr.Value);
                }
            }
            xml += "</tbl>";
            return xml;
        }
        string GetChildStatusTypeXmlForUpdate()
        {
            string xml = "<tbl>";
            xml += XMLField(ddlUpdateGroupCompany.SelectedValue);
            xml += "</tbl>";
            return xml;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (IsAnyItemChecked() || ddlUpdateGroupCompany.SelectedValue != "")
            {
                EssPL PL = new EssPL();
                if (ViewState["Mode"].ToString() == "Add")
                {
                    PL.OpCode = 64;
                    PL.XML = GetChildStatusXml();
                    PL.CreatedBy = Session["UserAutoId"].ToString();
                    EssDL.returnTable(PL);
                    if (!PL.isException)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record save successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                    }
                }
                else if (ViewState["Mode"].ToString() == "Edit")
                {
                    PL.OpCode = 65;
                    PL.AutoId = ddlEmpName.SelectedValue;
                    PL.String1 = ddlElement.SelectedValue;
                    PL.String2 = ddlAccessName.SelectedValue;
                    PL.String3 = ddlUpdateGroupCompany.SelectedValue;
                    PL.XML = GetChildStatusTypeXmlForUpdate();
                    PL.CreatedBy = Session["UserAutoId"].ToString();
                    EssDL.returnTable(PL);
                    if (!PL.isException)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record update successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                    }
                }
                Session["SuccessMessage"] = "ShowDone('Record(s) Save successfully');";
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Select atleast one Company');", true);
            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void ddlAccessName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlAccessName.SelectedValue != "")
            {
                foreach (ListItem item in chkGroupCompany.Items)
                {
                    if (item.Selected)
                    {
                        if (IsEntryExist(ddlEmpName.SelectedValue, ddlElement.SelectedValue, item.Value, ddlAccessName.SelectedValue) == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Exist Already!');", true);
                            ddlAccessName.SelectedIndex = -1;
                            divEmployeeAccess.Visible = false;
                            LV_Access_Menu_Company.DataSource = "";
                            LV_Access_Menu_Company.DataBind();
                        }
                        else
                        {
                            getAccessMenuList(ddlEmpName.SelectedValue, LV_Access_Menu_Company);
                        }
                    }
                }
            }
        }
        void getAccessMenuList(string id, ListView lv)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 68;
            PL.AutoId = id;
            PL.Type = 1;
            PL.String1 = 1;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                lv.DataSource = PL.dt;
                lv.DataBind();
                divEmployeeAccess.Visible = true;
            }
            else
            {
                lv.DataSource = "";
                lv.DataBind();
            }
        }
        protected void btnViewAction_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var item = (ListViewItem)btn.NamingContainer;

            hidEmpIdMain.Value = ((HiddenField)item.FindControl("hidEmpId")).Value;
            hidElementNameMain.Value = ((HiddenField)item.FindControl("hidElementName")).Value;
            hidAccessEmpIdMain.Value = ((HiddenField)item.FindControl("hidAccessEmpId")).Value;
            hidGroupIdMain.Value = ((HiddenField)item.FindControl("hidGroupId")).Value;
            SetForEditGroupWise();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openpp", "OpenPopUpAction();", true);
        }
        void SetForEditGroupWise()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 69;
            PL.AutoId = hidEmpIdMain.Value;
            PL.String1 = hidElementNameMain.Value;
            PL.String2 = hidAccessEmpIdMain.Value;
            PL.GroupId = hidGroupIdMain.Value;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                txtEndDateUpdate.Text = PL.dt.Rows[0]["EndDate"].ToString();
                if (PL.dt.Rows[0]["IsActive"].ToString() == "False")
                {
                    chkActiveUpdate.Checked = false;
                }
                else
                {
                    chkActiveUpdate.Checked = true;
                }
                getAccessMenuList(hidEmpIdMain.Value, LV_Access_Menu_Company_Update);
                foreach (DataRow row in dt.Rows)
                {
                    foreach (ListViewItem item2 in LV_Access_Menu_Company_Update.Items)
                    {
                        HiddenField hdnMenuid = (HiddenField)item2.FindControl("hidautoidUpdate");
                        if (hdnMenuid.Value == row["MenuId"].ToString())
                        {
                            CheckBox chkIsChecked = (CheckBox)item2.FindControl("chkIsCheckedUpdate");
                            if (row["MenuAccess"].ToString() == "True")
                            {
                                chkIsChecked.Checked = true;
                            }
                            else
                            {
                                chkIsChecked.Checked = false;
                            } 
                            break;
                        }
                    }
                }
            }
        }
        string XMLFieldUpdatePopUp(string GroupId)
        {
            string xml = "<tbl>";
            foreach (ListViewItem item in LV_Access_Menu_Company_Update.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkIsCheckedUpdate");
                if (chkSelect != null)
                {
                    CheckBox chkIsChecked = (CheckBox)item.FindControl("chkIsCheckedUpdate");
                    HiddenField hdnMenuId = (HiddenField)item.FindControl("hidautoidUpdate");
                    xml += "<tr>";
                    xml += "<EmpId><![CDATA[" + hidEmpIdMain.Value + "]]></EmpId>";
                    xml += "<ElementName><![CDATA[" + hidElementNameMain.Value + "]]></ElementName>";
                    xml += "<AccessEmpId><![CDATA[" + hidAccessEmpIdMain.Value + "]]></AccessEmpId>";
                    xml += "<EndDate><![CDATA[" + txtEndDateUpdate.Text.Trim() + "]]></EndDate>";
                    xml += "<IsActive><![CDATA[" + chkActiveUpdate.Checked + "]]></IsActive>";
                    xml += "<GroupId><![CDATA[" + GroupId + "]]></GroupId>";
                    xml += "<MenuId><![CDATA[" + hdnMenuId.Value + "]]></MenuId>";
                    xml += "<MenuAccess><![CDATA[" + (chkIsChecked.Checked == true ? 1 : 0) + "]]></MenuAccess>";
                    xml += "</tr>";
                }
            }
            xml += "</tbl>";
            return xml;
        }
        protected void btnUpdateAction_Click(object sender, EventArgs e)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 65;
            PL.AutoId = hidEmpIdMain.Value;
            PL.String1 = hidElementNameMain.Value;
            PL.String2 = hidAccessEmpIdMain.Value;
            PL.String3 = hidGroupIdMain.Value;
            PL.XML = XMLFieldUpdatePopUp(hidGroupIdMain.Value);
            PL.CreatedBy = Session["UserAutoId"].ToString();
            EssDL.returnTable(PL);
            if (!PL.isException)
            {
                FillListView(ddlGroupFilter.SelectedValue);
                divView.Visible = true;
                divAddEdit.Visible = false;
                clearField();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record update successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        } 
    }
}