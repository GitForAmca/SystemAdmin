﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemAdmin.App_Code;

namespace SystemAdmin.ESS
{
    public partial class TaskSchedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillGroup(ddlGroupFilter);
                FillTask(ddlTaskList);
                FillListView(ddlGroupFilter.SelectedValue);
            }
        }
        void FillGroup(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 60;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "GroupId";
            ddl.DataTextField = "Name";
            ddl.DataBind();
        }
        void FillTask(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 59;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "AutoId";
            ddl.DataTextField = "TaskName";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Option", ""));
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
            ddlUpdateGroupCompany.Items.Insert(0, new ListItem("Choose an item", ""));
        }
        void FillListView(string GroupId)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 43;
            PL.GroupId = GroupId;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_Operation_Mapping.DataSource = PL.dt;
                LV_Operation_Mapping.DataBind();
            }
            else
            {
                LV_Operation_Mapping.DataSource = "";
                LV_Operation_Mapping.DataBind();
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
            ClearField();
            ViewState["Mode"] = "Add";
        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV_Operation_Mapping.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        //-----------------
                        EssPL PL = new EssPL();
                        PL.OpCode = 43;
                        PL.AutoId = Autoid;
                        EssDL.returnTable(PL);
                        DataTable dt = PL.dt;
                        //--------------------------------
                        if (dt.Rows.Count > 0)
                        {
                            getDDLGroup();
                            ddlUpdateGroupCompany.SelectedIndex = ddlUpdateGroupCompany.Items.IndexOf(ddlUpdateGroupCompany.Items.FindByValue(dt.Rows[0]["GroupId"].ToString()));
                            ddlTaskList.SelectedIndex = ddlTaskList.Items.IndexOf(ddlTaskList.Items.FindByValue(dt.Rows[0]["TaskId"].ToString()));
                            txtStartTime.Text = dt.Rows[0]["StartTimeSame"].ToString();
                            txtEndTime.Text = dt.Rows[0]["EndTimeSame"].ToString();
                            if (PL.dt.Rows[0]["IsActive"].ToString() == "False")
                            {
                                chkActive.Checked = false;
                            }
                            else
                            {
                                chkActive.Checked = true;
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
            xml += "<GroupId><![CDATA[" + GroupId + "]]></GroupId>";
            xml += "<TaskId><![CDATA[" + ddlTaskList.SelectedValue + "]]></TaskId>";
            xml += "<StartTime><![CDATA[" + txtStartTime.Text.Trim() + "]]></StartTime>";
            xml += "<IsActive><![CDATA[" + chkActive.Checked + "]]></IsActive>";
            xml += "<EndTime><![CDATA[" + txtEndTime.Text.Trim() + "]]></EndTime>";
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
                    PL.OpCode = 41;
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
                    PL.OpCode = 42;
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
        protected void btncancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divAddEdit.Visible = false;
        }
        void ClearField()
        {
            ddlTaskList.SelectedIndex = -1;
            txtStartTime.Text = string.Empty;
            txtEndTime.Text = string.Empty;
        }

        [System.Web.Services.WebMethod]
        public static string CheckTask(string value, string oldname)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 40;
            PL.AutoId = Convert.ToInt32(value);
            PL.OldName = oldname;
            EssDL.returnTable(PL); ;
            return PL.dt.Rows[0]["count"].ToString();
        }
    }
}