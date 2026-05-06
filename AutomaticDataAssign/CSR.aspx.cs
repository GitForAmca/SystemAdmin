using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.AutomaticDataAssign
{
    public partial class CSR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillGroup(ddlGroupFilter);
                FillCSRFilter(ddlNameFilter);
                CEMList(ddlCEMFilter);
                FillListView(ddlGroupFilter.SelectedValue);
            }
        }
        void FillCSRFilter(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 81;
            PL.AutoId = ddlGroupFilter.SelectedValue;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "EmpId";
            ddl.DataTextField = "Name";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select an option", ""));
        }
        void FillGroup(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 80;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "GroupId";
            ddl.DataTextField = "Name";
            ddl.DataBind();
        }
        void FillAddCSR(DropDownList ddl)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 106;
            EssDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "Autoid";
            ddl.DataTextField = "CSRName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select an option", ""));
        }
        void CEMList(DropDownList ddl)
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
            ddlUpdateGroupCompany.Items.Insert(0, new ListItem("Select an option", ""));
        }
        void FillListView(string GroupId)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 105;
            PL.GroupId = GroupId;
            PL.String1 = ddlNameFilter.SelectedValue;
            PL.String2 = ddlCEMFilter.SelectedValue;
            PL.String3 = ddlStatusFilter.SelectedValue;
            PL.String4 = ddlIsAssignedFilter.SelectedValue;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_CEC.DataSource = PL.dt;
                LV_CEC.DataBind();
            }
            else
            {
                LV_CEC.DataSource = "";
                LV_CEC.DataBind();
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
            BindCheckBoxList();
            FillAddCSR(ddlAddEmpCSR);
            CEMList(ddlCEM);
            ClearField();
            ddlAddEmpCSR.Enabled = true;
            ViewState["Mode"] = "Add";
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_CEC.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        //-----------------
                        EssPL PL = new EssPL();
                        PL.OpCode = 105;
                        PL.AutoId = Autoid;
                        EssDL.returnTable(PL);
                        DataTable dt = PL.dt;
                        //--------------------------------
                        if (dt.Rows.Count > 0)
                        {
                            FillCSRFilter(ddlAddEmpCSR);
                            getDDLGroup();
                            CEMList(ddlCEM);
                            ddlUpdateGroupCompany.SelectedIndex = ddlUpdateGroupCompany.Items.IndexOf(ddlUpdateGroupCompany.Items.FindByValue(dt.Rows[0]["CompanyId"].ToString()));
                            ddlAddEmpCSR.Enabled = false;
                            ddlAddEmpCSR.SelectedIndex = ddlAddEmpCSR.Items.IndexOf(ddlAddEmpCSR.Items.FindByValue(dt.Rows[0]["EmpId"].ToString()));
                            ddlCEM.SelectedIndex = ddlCEM.Items.IndexOf(ddlCEM.Items.FindByValue(dt.Rows[0]["CEM"].ToString()));
                            if (PL.dt.Rows[0]["IsAssigned"].ToString() == "False")
                            {
                                chkIsAssigned.Checked = false;
                            }
                            else
                            {
                                chkIsAssigned.Checked = true;
                            }
                        }
                        ViewState["Mode"] = "Edit";
                        hidID.Value = Autoid.ToString();
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
        string XMLField(string GroupId)
        {
            string xml = "<tr>";
            xml += "<EmpId><![CDATA[" + ddlAddEmpCSR.SelectedValue + "]]></EmpId>";
            xml += "<DepartmentId><![CDATA[" + 42 + "]]></DepartmentId>";
            xml += "<Status><![CDATA[" + "Assign" + "]]></Status>";
            xml += "<CEM><![CDATA[" + ddlCEM.SelectedValue + "]]></CEM>";
            xml += "<IsAssigned><![CDATA[" + chkIsAssigned.Checked + "]]></IsAssigned>";
            xml += "<CompanyId><![CDATA[" + GroupId + "]]></CompanyId>";
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
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (IsAnyItemChecked() || ddlUpdateGroupCompany.SelectedValue != "")
            {
                EssPL PL = new EssPL();
                if (ViewState["Mode"].ToString() == "Add")
                {
                    PL.OpCode = 107;
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
                    if (CheckStatus(Convert.ToInt32(hidID.Value)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowError('Kindly set status Assign');", true);
                        return;
                    }
                    PL.OpCode = 108;
                    PL.AutoId = Convert.ToInt32(hidID.Value);
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
                divView.Visible = true;
                divAddEdit.Visible = false;
                FillListView(ddlGroupFilter.SelectedValue);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Select atleast one Company');", true);
            }

        }
        bool CheckStatus(int id)
        {
            bool a = false;
            EssPL PL = new EssPL();
            PL.OpCode = 105;
            PL.AutoId = id;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (dt.Rows[0]["Status"].ToString() == "NextAssign")
            {
                a = true;
            }
            else
            {
                a = false;
            }
            return a;
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divAddEdit.Visible = false;
        }
        void ClearField()
        {
            ddlAddEmpCSR.SelectedIndex = -1;
            ddlCEM.SelectedIndex = -1;
        }
        protected void btn_next_assigned_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (ListViewItem)btn.NamingContainer;

            EssPL PL = new EssPL();
            PL.String1 = Convert.ToInt32(((HiddenField)item.FindControl("hdnCEM")).Value);
            PL.String2 = Convert.ToInt32(((HiddenField)item.FindControl("hdnCompanyId")).Value);
            PL.AutoId = Convert.ToInt32(((HiddenField)item.FindControl("hdnAutoId")).Value);
            PL.OpCode = 109;
            EssDL.returnTable(PL);
            if (!PL.isException)
            {
                FillListView(ddlGroupFilter.SelectedValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowDone('Record(s) updated successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
            }
        }
    }
}