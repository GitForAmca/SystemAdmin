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
    public partial class CRMMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillGroup(ddlGroupFilter);
                FillCRM();
                FillListView(ddlGroupFilter.SelectedValue);
            }
        }
        void FillCRM()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 86;
            PL.AutoId = ddlGroupFilter.SelectedValue;
            DropdownDL.returnTable(PL);
            ddlFromCRMFilter.DataSource = PL.dt;
            ddlFromCRMFilter.DataValueField = "Autoid";
            ddlFromCRMFilter.DataTextField = "Name";
            ddlFromCRMFilter.DataBind();
            ddlFromCRMFilter.Items.Insert(0, new ListItem("Select an option", ""));
            ddlToCRMFilter.DataSource = PL.dt;
            ddlToCRMFilter.DataValueField = "Autoid";
            ddlToCRMFilter.DataTextField = "Name";
            ddlToCRMFilter.DataBind();
            ddlToCRMFilter.Items.Insert(0, new ListItem("Select an option", ""));
            ddlFromCRM.DataSource = PL.dt;
            ddlFromCRM.DataValueField = "Autoid";
            ddlFromCRM.DataTextField = "Name";
            ddlFromCRM.DataBind();
            ddlFromCRM.Items.Insert(0, new ListItem("Select an option", ""));
            ddlToCRM.DataSource = PL.dt;
            ddlToCRM.DataValueField = "Autoid";
            ddlToCRM.DataTextField = "Name";
            ddlToCRM.DataBind();
            ddlToCRM.Items.Insert(0, new ListItem("Select an option", ""));
        }
        void FillGroup(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 85;
            DropdownDL.returnTable(PL);
            ddl.DataSource = PL.dt;
            ddl.DataValueField = "GroupId";
            ddl.DataTextField = "Name";
            ddl.DataBind();
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
            PL.OpCode = 151;
            PL.GroupId = GroupId;
            PL.String1 = ddlAssignedTypeFilter.SelectedValue;
            PL.String2 = ddlFromCRMFilter.SelectedValue;
            PL.String3 = ddlToCRMFilter.SelectedValue;
            EssDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                LV_CRM_Mapping.DataSource = PL.dt;
                LV_CRM_Mapping.DataBind();
            }
            else
            {
                LV_CRM_Mapping.DataSource = "";
                LV_CRM_Mapping.DataBind();
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
            foreach (ListViewItem item in LV_CRM_Mapping.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        //-----------------
                        EssPL PL = new EssPL();
                        PL.OpCode = 151;
                        PL.AutoId = Autoid;
                        EssDL.returnTable(PL);
                        DataTable dt = PL.dt;
                        //--------------------------------
                        if (dt.Rows.Count > 0)
                        {
                            getDDLGroup();
                            ddlUpdateGroupCompany.SelectedIndex = ddlUpdateGroupCompany.Items.IndexOf(ddlUpdateGroupCompany.Items.FindByValue(dt.Rows[0]["GroupId"].ToString()));
                            ddlFromCRM.SelectedIndex = ddlFromCRM.Items.IndexOf(ddlFromCRM.Items.FindByValue(dt.Rows[0]["FromCRM"].ToString()));
                            ddlToCRM.SelectedIndex = ddlToCRM.Items.IndexOf(ddlToCRM.Items.FindByValue(dt.Rows[0]["ToCRM"].ToString()));
                            ddlAssignedType.SelectedIndex = ddlAssignedType.Items.IndexOf(ddlAssignedType.Items.FindByValue(dt.Rows[0]["AssignedType"].ToString()));
                            if (PL.dt.Rows[0]["IsActive"].ToString() == "False")
                            {
                                chkIsActive.Checked = false;
                            }
                            else
                            {
                                chkIsActive.Checked = true;
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
            xml += "<FromCRM><![CDATA[" + ddlFromCRM.SelectedValue + "]]></FromCRM>";
            xml += "<ToCRM><![CDATA[" + ddlToCRM.SelectedValue + "]]></ToCRM>";
            xml += "<AssignedType><![CDATA[" + ddlAssignedType.SelectedValue + "]]></AssignedType>";
            xml += "<IsActive><![CDATA[" + chkIsActive.Checked + "]]></IsActive>";
            xml += "<GroupId><![CDATA[" + GroupId + "]]></GroupId>";
            xml += "</tr>";
            return xml;
        }
        string XMLFieldForupdate()
        {
            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<FromCRM><![CDATA[" + ddlFromCRM.SelectedValue + "]]></FromCRM>";
            xml += "<ToCRM><![CDATA[" + ddlToCRM.SelectedValue + "]]></ToCRM>";
            xml += "<AssignedType><![CDATA[" + ddlAssignedType.SelectedValue + "]]></AssignedType>";
            xml += "<IsActive><![CDATA[" + chkIsActive.Checked + "]]></IsActive>";
            xml += "</tr>";
            xml += "</tbl>";
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
                    PL.OpCode = 152;
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
                    PL.OpCode = 153;
                    PL.AutoId = Convert.ToInt32(hidID.Value);
                    PL.XML1 = XMLFieldForupdate();
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
            Response.Redirect(Request.RawUrl);
        }
        void ClearField()
        {
            ddlFromCRM.SelectedIndex = -1;
            ddlToCRM.SelectedIndex = -1;
            ddlAssignedType.SelectedIndex = -1;
        }
    }
}