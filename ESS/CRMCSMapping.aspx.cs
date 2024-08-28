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
    public partial class CRMCSMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillGroup();
                FillListView(ddlGroupFilter.SelectedValue);
                CSRList();
                CECList();
                CEMList();
            }
        }
        void CSRList()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 31;
            DropdownDL.returnTable(PL);
            ddlCSR.DataValueField = "Autoid";
            ddlCSR.DataTextField = "Name";
            ddlCSR.DataSource = PL.dt;
            ddlCSR.DataBind();
            ddlCSR.Items.Insert(0, new ListItem("Select CSR", ""));
        }
        void CECList()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 32;
            DropdownDL.returnTable(PL);
            ddlCEC.DataValueField = "Autoid";
            ddlCEC.DataTextField = "Name";
            ddlCEC.DataSource = PL.dt;
            ddlCEC.DataBind();
            ddlCEC.Items.Insert(0, new ListItem("Select CEC", ""));
        }
        void CEMList()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 33;
            DropdownDL.returnTable(PL);
            ddlCEM.DataValueField = "Autoid";
            ddlCEM.DataTextField = "CEM";
            ddlCEM.DataSource = PL.dt;
            ddlCEM.DataBind();
            ddlCEM.Items.Insert(0, new ListItem("Select CEM", ""));
        }
        void FillGroup()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 34;
            DropdownDL.returnTable(PL);
            ddlGroupFilter.DataSource = PL.dt;
            ddlGroupFilter.DataValueField = "GroupId";
            ddlGroupFilter.DataTextField = "Name";
            ddlGroupFilter.DataBind();
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
        void FillListView(string GroupId)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 4;
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
                        PL.OpCode = 4;
                        PL.AutoId = Autoid;
                        EssDL.returnTable(PL);
                        DataTable dt = PL.dt;
                        //--------------------------------
                        if (dt.Rows.Count > 0)
                        {
                            getDDLGroup();
                            ddlUpdateGroupCompany.SelectedIndex = ddlUpdateGroupCompany.Items.IndexOf(ddlUpdateGroupCompany.Items.FindByValue(dt.Rows[0]["GroupId"].ToString()));
                            ddlCSR.SelectedIndex = ddlCSR.Items.IndexOf(ddlCSR.Items.FindByValue(dt.Rows[0]["CSR"].ToString()));
                            ddlCEC.SelectedIndex = ddlCEC.Items.IndexOf(ddlCEC.Items.FindByValue(dt.Rows[0]["CEC"].ToString()));
                            ddlCEM.SelectedIndex = ddlCEM.Items.IndexOf(ddlCEM.Items.FindByValue(dt.Rows[0]["CEM"].ToString()));
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
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView(ddlGroupFilter.SelectedValue);
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
        string XMLField(string GroupId)
        {
            string xml = "<tr>";
            xml += "<GroupId><![CDATA[" + GroupId + "]]></GroupId>";
            xml += "<CEC><![CDATA[" + ddlCEC.SelectedValue + "]]></CEC>";
            xml += "<CSR><![CDATA[" + ddlCSR.SelectedValue + "]]></CSR>";
            xml += "<CEM><![CDATA[" + ddlCEM.SelectedValue + "]]></CEM>";
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
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (IsAnyItemChecked() || ddlUpdateGroupCompany.SelectedValue != "")
            {
                EssPL PL = new EssPL();
                if (ViewState["Mode"].ToString() == "Add")
                {
                    PL.OpCode = 5;
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
                    PL.OpCode = 6;
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
            ddlCSR.SelectedIndex = -1;
            ddlCEC.SelectedIndex = -1;
            ddlCEM.SelectedIndex = -1;
        }

        [System.Web.Services.WebMethod]
        public static string CheckName(string value, string oldname)
        {
            EssPL PL = new EssPL();
            PL.OpCode = 7;
            PL.AutoId = Convert.ToInt32(value);
            PL.OldName = oldname;
            EssDL.returnTable(PL); ;
            return PL.dt.Rows[0]["count"].ToString();
        }
    }
}