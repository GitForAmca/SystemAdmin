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
    public partial class ServiceTypeMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserAutoId"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                FillGroup();
                FillListView();
                FillOperation();
                FillEA();
            }
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
        void ddlGetGroup()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 3;
            PL.AutoId = 1;
            DropdownDL.returnTable(PL);
            ddlUpdateGroupCompany.DataSource = PL.dt;
            ddlUpdateGroupCompany.DataValueField = "AutoId";
            ddlUpdateGroupCompany.DataTextField = "Name";
            ddlUpdateGroupCompany.DataBind();
        }
        void FillListView()
        {
            //-----------------
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 1;
            PL.GroupId = ddlGroupFilter.SelectedValue;
            ServiceMasterDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            LV.DataSource = dt;
            LV.DataBind();
        }


        void FillGroup()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 20;
            DropdownDL.returnTable(PL);
            ddlGroupFilter.DataSource = PL.dt;
            ddlGroupFilter.DataValueField = "GroupId";
            ddlGroupFilter.DataTextField = "Name";
            ddlGroupFilter.DataBind();
        }
        void FillEA()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 22;
            DropdownDL.returnTable(PL);
            ddlPrimaryEA.DataSource = PL.dt;
            ddlPrimaryEA.DataValueField = "Autoid";
            ddlPrimaryEA.DataTextField = "Name";
            ddlPrimaryEA.DataBind();
            ddlPrimaryEA.Items.Insert(0, new ListItem("Select Option", ""));
            /////////////////////////
            ddlSecondaryEA.DataSource = PL.dt;
            ddlSecondaryEA.DataValueField = "Autoid";
            ddlSecondaryEA.DataTextField = "Name";
            ddlSecondaryEA.DataBind();
            ddlSecondaryEA.Items.Insert(0, new ListItem("Select Option", ""));
        }
        void FillOperation()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 21;
            DropdownDL.returnTable(PL);
            ddlManager.DataSource = PL.dt;
            ddlManager.DataValueField = "Autoid";
            ddlManager.DataTextField = "Name";
            ddlManager.DataBind();
            ddlManager.Items.Insert(0, new ListItem("Select Option", ""));
            /////////////////////////
            ddlAsstManager.DataSource = PL.dt;
            ddlAsstManager.DataValueField = "Autoid";
            ddlAsstManager.DataTextField = "Name";
            ddlAsstManager.DataBind();
            ddlAsstManager.Items.Insert(0, new ListItem("Select Option", ""));
            /////////////////////////
            ddlPrimarySupervisor.DataSource = PL.dt;
            ddlPrimarySupervisor.DataValueField = "Autoid";
            ddlPrimarySupervisor.DataTextField = "Name";
            ddlPrimarySupervisor.DataBind();
            ddlPrimarySupervisor.Items.Insert(0, new ListItem("Select Option", ""));
            /////////////////////////
            ddlSecondarySupervisor.DataSource = PL.dt;
            ddlSecondarySupervisor.DataValueField = "Autoid";
            ddlSecondarySupervisor.DataTextField = "Name";
            ddlSecondarySupervisor.DataBind();
            ddlSecondarySupervisor.Items.Insert(0, new ListItem("Select Option", ""));
            /////////////////////////
            ddlPrimaryEA.DataSource = PL.dt;
            ddlPrimaryEA.DataValueField = "Autoid";
            ddlPrimaryEA.DataTextField = "Name";
            ddlPrimaryEA.DataBind();
            ddlPrimaryEA.Items.Insert(0, new ListItem("Select Option", ""));
            /////////////////////////
            ddlSecondaryEA.DataSource = PL.dt;
            ddlSecondaryEA.DataValueField = "Autoid";
            ddlSecondaryEA.DataTextField = "Name";
            ddlSecondaryEA.DataBind();
            ddlSecondaryEA.Items.Insert(0, new ListItem("Select Option", ""));
            /////////////////////////
            ddlPrimaryAssigner.DataSource = PL.dt;
            ddlPrimaryAssigner.DataValueField = "Autoid";
            ddlPrimaryAssigner.DataTextField = "Name";
            ddlPrimaryAssigner.DataBind();
            ddlPrimaryAssigner.Items.Insert(0, new ListItem("Select Option", ""));
            /////////////////////////
            ddlSecondaryAssigner.DataSource = PL.dt;
            ddlSecondaryAssigner.DataValueField = "Autoid";
            ddlSecondaryAssigner.DataTextField = "Name";
            ddlSecondaryAssigner.DataBind();
            ddlSecondaryAssigner.Items.Insert(0, new ListItem("Select Option", ""));
            /////////////////////////
            ddlPrimaryReviewer.DataSource = PL.dt;
            ddlPrimaryReviewer.DataValueField = "Autoid";
            ddlPrimaryReviewer.DataTextField = "Name";
            ddlPrimaryReviewer.DataBind();
            ddlPrimaryReviewer.Items.Insert(0, new ListItem("Select Option", ""));
            /////////////////////////
            ddlSecondaryReviewer.DataSource = PL.dt;
            ddlSecondaryReviewer.DataValueField = "Autoid";
            ddlSecondaryReviewer.DataTextField = "Name";
            ddlSecondaryReviewer.DataBind();
            ddlSecondaryReviewer.Items.Insert(0, new ListItem("Select Option", ""));
        }

        void ClearField()
        {
            txtServiceName.Text = "";
            ddlManager.SelectedIndex = -1;
            ddlAsstManager.SelectedIndex = -1;
            ddlPrimarySupervisor.SelectedIndex = -1;
            ddlSecondarySupervisor.SelectedIndex = -1;
            ddlPrimaryEA.SelectedIndex = -1;
            ddlSecondaryEA.SelectedIndex = -1;
            ddlPrimaryAssigner.SelectedIndex = -1;
            ddlSecondaryAssigner.SelectedIndex = -1;
            ddlPrimaryReviewer.SelectedIndex = -1;
            ddlSecondaryReviewer.SelectedIndex = -1;
            chkactive.Checked = true;
            hidID.Value = "";
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            FillListView();
        }
        protected void lnkBtnAddNew_Click(object sender, EventArgs e)
        {
            ClearField();
            divView.Visible = false;
            divAddEdit.Visible = true;
            ViewState["Mode"] = "Add";
            divUpdateGroup.Visible = false;
            divAddGroup.Visible = true;
            BindCheckBoxList();
            ddlUpdateGroupCompany.Items.Clear();
        }
        protected void ddlUpdateGroupCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            setForEdit(Convert.ToInt32(ddlUpdateGroupCompany.SelectedValue), Convert.ToInt32(hidID.Value));
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
                        ddlGetGroup();
                        divUpdateGroup.Visible = true;
                        divAddGroup.Visible = false;
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        hidGroupID.Value = chkSelect.Attributes["GroupId"];
                        hidID.Value = Autoid.ToString();
                        //-----------------
                        setForEdit(Convert.ToInt32(hidGroupID.Value), Autoid);
                    }
                }
            }
        }
        void setForEdit(int groupId, int id)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 1;
            PL.GroupId = groupId;
            PL.AutoId = id;
            ServiceMasterDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            if (dt.Rows.Count > 0)
            {
                txtServiceName.Text = dt.Rows[0]["ServiceTypeName"].ToString();
                txtServiceName.Attributes.Add("oldname", dt.Rows[0]["ServiceTypeName"].ToString());
                ddlUpdateGroupCompany.SelectedIndex = ddlUpdateGroupCompany.Items.IndexOf(ddlUpdateGroupCompany.Items.FindByValue(PL.dt.Rows[0]["GroupId"].ToString()));
                ddlManager.SelectedIndex = ddlManager.Items.IndexOf(ddlManager.Items.FindByValue(PL.dt.Rows[0]["Manager"].ToString()));
                ddlAsstManager.SelectedIndex = ddlAsstManager.Items.IndexOf(ddlAsstManager.Items.FindByValue(PL.dt.Rows[0]["AsstManager"].ToString()));
                ddlPrimarySupervisor.SelectedIndex = ddlPrimarySupervisor.Items.IndexOf(ddlPrimarySupervisor.Items.FindByValue(PL.dt.Rows[0]["Supervisor"].ToString()));
                ddlSecondarySupervisor.SelectedIndex = ddlSecondarySupervisor.Items.IndexOf(ddlSecondarySupervisor.Items.FindByValue(PL.dt.Rows[0]["SecondarySupervisor"].ToString()));
                ddlPrimaryEA.SelectedIndex = ddlPrimaryEA.Items.IndexOf(ddlPrimaryEA.Items.FindByValue(PL.dt.Rows[0]["PrimaryAccessTo"].ToString()));
                ddlSecondaryEA.SelectedIndex = ddlSecondaryEA.Items.IndexOf(ddlSecondaryEA.Items.FindByValue(PL.dt.Rows[0]["SecondaryAccessTo"].ToString()));
                ddlPrimaryAssigner.SelectedIndex = ddlPrimaryAssigner.Items.IndexOf(ddlPrimaryAssigner.Items.FindByValue(PL.dt.Rows[0]["Assigner"].ToString()));
                ddlSecondaryAssigner.SelectedIndex = ddlSecondaryAssigner.Items.IndexOf(ddlSecondaryAssigner.Items.FindByValue(PL.dt.Rows[0]["SecondaryAssigner"].ToString()));
                ddlPrimaryReviewer.SelectedIndex = ddlPrimaryReviewer.Items.IndexOf(ddlPrimaryReviewer.Items.FindByValue(PL.dt.Rows[0]["PrimaryReviewer"].ToString()));
                ddlSecondaryReviewer.SelectedIndex = ddlSecondaryReviewer.Items.IndexOf(ddlSecondaryReviewer.Items.FindByValue(PL.dt.Rows[0]["SecondaryReviewer"].ToString()));
                chkactive.Checked = bool.Parse(dt.Rows[0]["isActive"].ToString());
                ViewState["Mode"] = "Edit";
                divView.Visible = false;
                divAddEdit.Visible = true;
            }
            else
            {
                ClearField();
            }
        }
        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LV.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        int Autoid = Convert.ToInt32(chkSelect.Attributes["Autoid"]);
                        //-----------------
                        ServiceMasterPL PL = new ServiceMasterPL();
                        PL.OpCode = 00;
                        PL.AutoId = Autoid;
                        ServiceMasterDL.returnTable(PL);
                        if (!PL.isException)
                        {
                            divView.Visible = true;
                            divAddEdit.Visible = false;
                            ClearField();
                            FillListView();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "flagSave", "ShowDone('Record delete successfully');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "flagError", "ShowError('" + PL.exceptionMessage + "');", true);
                        }
                    }
                }
            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            divView.Visible = true;
            divAddEdit.Visible = false;
        }
        string XMLField(string GroupId)
        {
            string xml = "<tr>";
            xml += "<tr>";
            xml += "<Manager><![CDATA[" + ddlManager.SelectedValue + "]]></Manager>";
            xml += "<AsstManager><![CDATA[" + ddlAsstManager.SelectedValue + "]]></AsstManager>";
            xml += "<Supervisor><![CDATA[" + ddlPrimarySupervisor.SelectedValue + "]]></Supervisor>";
            xml += "<SecondarySupervisor><![CDATA[" + ddlSecondarySupervisor.SelectedValue + "]]></SecondarySupervisor>";
            xml += "<PrimaryAccessTo><![CDATA[" + ddlPrimaryEA.SelectedValue + "]]></PrimaryAccessTo>";
            xml += "<SecondaryAccessTo><![CDATA[" + ddlSecondaryEA.SelectedValue + "]]></SecondaryAccessTo>";
            xml += "<Assigner><![CDATA[" + ddlPrimaryAssigner.SelectedValue + "]]></Assigner>";
            xml += "<SecondaryAssigner><![CDATA[" + ddlSecondaryAssigner.SelectedValue + "]]></SecondaryAssigner>";
            xml += "<PrimaryReviewer><![CDATA[" + ddlPrimaryReviewer.SelectedValue + "]]></PrimaryReviewer>";
            xml += "<SecondaryReviewer><![CDATA[" + ddlSecondaryReviewer.SelectedValue + "]]></SecondaryReviewer>";
            xml += "<GroupId><![CDATA[" + GroupId + "]]></GroupId>";
            xml += "<IsActive><![CDATA[" + (chkactive.Checked) + "]]></IsActive>";
            xml += "</tr>";
            return xml;
        }
        string GetChildServiceTypeXml()
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
        string GetChildServiceTypeXmlForUpdate()
        {
            string xml = "<tbl>";
            xml += XMLField(ddlUpdateGroupCompany.SelectedValue);
            xml += "</tbl>";
            return xml;
        }
        string GetParentServiceTypeXml()
        {
            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<ServiceTypeName><![CDATA[" + txtServiceName.Text.Trim() + "]]></ServiceTypeName>";
            xml += "</tr>";
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
                ServiceMasterPL PL = new ServiceMasterPL();
                if (ViewState["Mode"].ToString() == "Add")
                {
                    PL.OpCode = 2;
                    PL.XML = GetParentServiceTypeXml();
                    PL.XML1 = GetChildServiceTypeXml();
                    PL.CreatedBy = Session["UserAutoId"].ToString();
                    ServiceMasterDL.returnTable(PL);
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
                    PL.GroupId = Convert.ToInt32(ddlUpdateGroupCompany.SelectedValue);
                    PL.XML = GetParentServiceTypeXml();
                    PL.XML1 = GetChildServiceTypeXmlForUpdate();
                    PL.CreatedBy = Session["UserAutoId"].ToString();
                    ServiceMasterDL.returnTable(PL);
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
                FillGroup();
                FillListView();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Select atleast one Company');", true);
            }
        }
        [System.Web.Services.WebMethod]
        public static string CheckName(string text, string oldname)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 4;
            PL.Type = text;
            PL.OldName = oldname;
            ServiceMasterDL.returnTable(PL); ;
            return PL.dt.Rows[0]["count"].ToString();
        }
    }
}