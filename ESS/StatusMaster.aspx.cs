using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.ESS
{
    public partial class StatusMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getIndustry(ddlIndustries);
                getIndustry(ddlIndustriesFilter);
                ddlIndustriesFilter.SelectedValue = "1";
                getDepartmentddl(ddlIndustriesFilter.SelectedValue);
                FillGroup(ddlIndustriesFilter.SelectedValue);
                getStatusType(ddlTypeSave);
                getStatusType(ddlTypeSearch);
                getStatusAction(ddlActionSave);
                getStatusAction(ddlActionSearch);
                getStatusCategory(ddlCategorySave);
                getStatusCategory(ddlCategorySearch);
                FillListView();
            }
        }
        void FillGroup(string industry)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 30;
            PL.AutoId = industry;
            DropdownDL.returnTable(PL);
            ddlGroupFilter.DataSource = PL.dt;
            ddlGroupFilter.DataValueField = "GroupId";
            ddlGroupFilter.DataTextField = "Name";
            ddlGroupFilter.DataBind();
        }
        void getIndustry(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 2;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "ID";
            ddl.DataTextField = "Description";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getStatusType(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 27;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Type";
            ddl.DataTextField = "Type";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getStatusAction(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 28;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Action";
            ddl.DataTextField = "Action";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getStatusCategory(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 29;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Category";
            ddl.DataTextField = "Category";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        protected void ddlIndustries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIndustries.SelectedValue != "")
            {
                divAddGroup.Visible = true;
                getDepartment(ddlIndustries.SelectedValue);
                BindCheckBoxList(ddlIndustries.SelectedValue);
            }
            else
            {
                divAddGroup.Visible = false;
            }
        }
        protected void ddlIndustriesFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIndustriesFilter.SelectedValue != "")
            {
                FillGroup(ddlIndustriesFilter.SelectedValue);
                getDepartmentddl(ddlIndustriesFilter.SelectedValue);
            }
        }
        void getDepartmentddl(string IndustryId)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 26;
            PL.IndustryId = IndustryId;
            DropdownDL.returnTable(PL);
            lstdepartmentsearch.DataSource = PL.dt;
            lstdepartmentsearch.DataValueField = "Autoid";
            lstdepartmentsearch.DataTextField = "Name";
            lstdepartmentsearch.DataBind();
            lstdepartmentsearch.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getDepartment(string IndustryId)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 26;
            PL.IndustryId = IndustryId;
            DropdownDL.returnTable(PL);
            lstDepartmentSave.DataSource = PL.dt;
            lstDepartmentSave.DataValueField = "Autoid";
            lstDepartmentSave.DataTextField = "Name";
            lstDepartmentSave.DataBind();
            lstDepartmentSave.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void getDDLGroup(string IndustryId)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 3;
            PL.AutoId = IndustryId;
            DropdownDL.returnTable(PL);
            ddlUpdateGroupCompany.DataSource = PL.dt;
            ddlUpdateGroupCompany.DataValueField = "Autoid";
            ddlUpdateGroupCompany.DataTextField = "Name";
            ddlUpdateGroupCompany.DataBind();
            ddlUpdateGroupCompany.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            divView.Visible = false;
            divAddEdit.Visible = true;
            divAddGroup.Visible = true;
            divUpdateGroup.Visible = false;
            ddlIndustries.Enabled = true;
            ddlUpdateGroupCompany.Enabled = true;
            ClearField();
            ViewState["Mode"] = "Add";
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_StatusBox.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        divView.Visible = true;
                        divAddEdit.Visible = false;
                        ViewState["Mode"] = "Edit";
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        hidID.Value = Autoid.ToString();
                        //-----------------
                        setForEdit(Autoid);
                    }
                }
            }
        }
        void setForEdit(int id)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 2;
            PL.AutoId = id;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            if (dt.Rows.Count > 0)
            {
                txtStatusName.Text = dt.Rows[0]["StatusName"].ToString();
                getDepartment(dt.Rows[0]["Industry"].ToString());
                SetMultipledep(lstDepartmentSave, dt.Rows[0]["DepartmentId"].ToString());
                getDDLGroup(dt.Rows[0]["Industry"].ToString());
                ddlUpdateGroupCompany.SelectedIndex = ddlUpdateGroupCompany.Items.IndexOf(ddlUpdateGroupCompany.Items.FindByValue(PL.dt.Rows[0]["GroupId"].ToString()));
                ddlIndustries.SelectedIndex = ddlIndustries.Items.IndexOf(ddlIndustries.Items.FindByValue(PL.dt.Rows[0]["Industry"].ToString()));
                ddlTypeSave.SelectedIndex = ddlTypeSave.Items.IndexOf(ddlTypeSave.Items.FindByValue(PL.dt.Rows[0]["Type"].ToString()));
                ddlActionSave.SelectedIndex = ddlActionSave.Items.IndexOf(ddlActionSave.Items.FindByValue(PL.dt.Rows[0]["Action"].ToString()));
                ddlCategorySave.SelectedIndex = ddlCategorySave.Items.IndexOf(ddlCategorySave.Items.FindByValue(PL.dt.Rows[0]["Category"].ToString()));
                chckIsActive.Checked = bool.Parse(dt.Rows[0]["IsActive"].ToString());
                ViewState["Mode"] = "Edit";
                divView.Visible = false;
                divAddEdit.Visible = true;
                divAddGroup.Visible = false;
                divUpdateGroup.Visible = true;
                ddlIndustries.Enabled = false;
                ddlUpdateGroupCompany.Enabled = false;
            }
            else
            {
                ClearField();
            }
        }
        void SetMultipledep(ListBox ddl, string ids)
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
        void FillListView()
        {
            EssPL PL = new EssPL();
            PL.OpCode = 2;
            PL.Industry = ddlIndustriesFilter.SelectedValue;
            PL.GroupId = ddlGroupFilter.SelectedValue;
            PL.Department = lstdepartmentsearch.SelectedValue;
            PL.Type = ddlTypeSearch.SelectedValue;
            PL.Action = ddlActionSearch.SelectedValue;
            PL.Category = ddlCategorySearch.SelectedValue;
            PL.IsActive = ddlIsActive.SelectedValue;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if(PL.dt.Rows.Count > 0)
            {
                LV_StatusBox.DataSource = PL.dt;
                LV_StatusBox.DataBind();
            }
            else
            {
                LV_StatusBox.DataSource = "";
                LV_StatusBox.DataBind();
            }
        }
        void ClearField()
        {
            ddlIndustries.SelectedIndex = -1;
            txtStatusName.Text = string.Empty;
            lstDepartmentSave.SelectedIndex = -1;
            ddlTypeSave.SelectedIndex = -1;
            ddlActionSave.SelectedIndex = -1;
            ddlCategorySave.SelectedIndex = -1;
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
        string GetParentStatusXml()
        {
            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<StatusName><![CDATA[" + txtStatusName.Text.Trim() + "]]></StatusName>";
            xml += "<Department><![CDATA[" + Request.Form[lstDepartmentSave.UniqueID] + "]]></Department>";
            xml += "<Type><![CDATA[" + ddlTypeSave.SelectedValue + "]]></Type>";
            xml += "<Action><![CDATA[" + ddlActionSave.SelectedValue + "]]></Action>";
            xml += "<Category><![CDATA[" + ddlCategorySave.SelectedValue + "]]></Category>";
            xml += "<Industry><![CDATA[" + ddlIndustries.SelectedValue + "]]></Industry>";
            xml += "</tr>";
            xml += "</tbl>";
            return xml;
        }
        string XMLField(string GroupId)
        {
            string xml = "<tr>";
            xml += "<GroupId><![CDATA[" + GroupId + "]]></GroupId>";
            xml += "<IsActive><![CDATA[" + (chckIsActive.Checked) + "]]></IsActive>";
            xml += "</tr>";
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsAnyItemChecked() || ddlUpdateGroupCompany.SelectedValue != "")
            {
                EssPL PL = new EssPL();
                if (ViewState["Mode"].ToString() == "Add")
                {
                    PL.OpCode = 1;
                    PL.XML = GetParentStatusXml();
                    PL.XML1 = GetChildStatusXml();
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
                    PL.OpCode = 3;
                    PL.AutoId = Convert.ToInt32(hidID.Value);
                    PL.XML = GetParentStatusXml();
                    PL.XML1 = GetChildStatusTypeXmlForUpdate();
                    PL.CreatedBy = Session["UserAutoId"].ToString();
                    EssDL.returnTable(PL);
                    if (!PL.isException)
                    {
                        divView.Visible = true;
                        divAddEdit.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record update successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                    }
                }
                ClearField();
                FillListView();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Select atleast one Company');", true);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divAddEdit.Visible = false;
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
        private void BindCheckBoxList(string IndustyId)
        {
            chkGroupCompany.DataSource = GetGroup(IndustyId);
            chkGroupCompany.DataBind();
        }
        protected void chkGroupCompany_DataBinding(object sender, EventArgs e)
        {
            ((CheckBoxList)sender).DataSource = GetGroup(ddlIndustries.SelectedValue);
        }
        public DataTable GetGroup(string IndustyId)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 3;
            PL.AutoId = IndustyId;
            DropdownDL.returnTable(PL);
            return PL.dt;
        }
    }
}