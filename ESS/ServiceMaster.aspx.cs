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
    public partial class ServiceMaster : System.Web.UI.Page
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
                GetServiceType(ddlServiceType);
                GetAssignmentType();
                FillListView();
                BindNo();
            }
        }
        void BindNo()
        {
            for (int i = 1; i <= 365; i++)
            {
                string val = i.ToString();
                ddlConsultantETC.Items.Add(new ListItem(val, val));
                ddlClientETC.Items.Add(new ListItem(val, val));
            }
            ddlConsultantETC.Items.Insert(0, new ListItem("Select Option", ""));
            ddlClientETC.Items.Insert(0, new ListItem("Select Option", ""));
        }
        void GetServiceType(DropDownList ddl)
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 24;
            DropdownDL.returnTable(PL);
            ddl.DataTextField = "ServiceTypeName";
            ddl.DataValueField = "Autoid";
            ddl.DataSource = PL.dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Option", ""));
        }
        void GetAssignmentType()
        {
            DropdownPL PL = new DropdownPL();
            PL.OpCode = 25;
            DropdownDL.returnTable(PL);
            ddlAssignmentPeriod.DataTextField = "ServiceTypeName";
            ddlAssignmentPeriod.DataValueField = "Autoid";
            ddlAssignmentPeriod.DataSource = PL.dt;
            ddlAssignmentPeriod.DataBind();
            ddlAssignmentPeriod.Items.Insert(0, new ListItem("Select Option", ""));

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
            PL.OpCode = 5;
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
            PL.OpCode = 23;
            DropdownDL.returnTable(PL);
            ddlGroupFilter.DataSource = PL.dt;
            ddlGroupFilter.DataValueField = "GroupId";
            ddlGroupFilter.DataTextField = "Name";
            ddlGroupFilter.DataBind();
        }

        void ClearField()
        {
            txtServiceName.Text = "";
            txtServiceName.Attributes.Add("oldname", "");
            ddlServiceType.SelectedIndex = -1;
            ddlConsultantETC.SelectedIndex = -1;
            ddlClientETC.SelectedIndex = -1;
            ddlQuestionnaire.SelectedIndex = -1;
            chkactive.Checked = true;
            txtMainRemarks.Text = "";
            txtConsultantPrice.Text = "0.00";
            txtClientprice.Text = "0.00";
            txtAuthorityFee.Text = "0.00";
            txtConsultantDescription.Text = "";
            txtClientDescription.Text = "";
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
            FillQuesListView(ddlQuestionnaire.SelectedValue);
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
            PL.OpCode = 5;
            PL.GroupId = groupId;
            PL.AutoId = id;
            ServiceMasterDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            if (dt.Rows.Count > 0)
            {
                txtServiceName.Text = dt.Rows[0]["ServiceName"].ToString();
                ddlUpdateGroupCompany.SelectedIndex = ddlUpdateGroupCompany.Items.IndexOf(ddlUpdateGroupCompany.Items.FindByValue(PL.dt.Rows[0]["GroupId"].ToString()));

                ddlServiceType.SelectedIndex = ddlServiceType.Items.IndexOf(ddlServiceType.Items.FindByValue(dt.Rows[0]["ServiceTypeAutoid"].ToString()));
                ddlQuestionnaire.SelectedIndex = ddlQuestionnaire.Items.IndexOf(ddlQuestionnaire.Items.FindByValue(dt.Rows[0]["Questionnaire"].ToString()));
                txtMainRemarks.Text = dt.Rows[0]["MainRemarks"].ToString();
                txtConsultantPrice.Text = dt.Rows[0]["ConsultantPrice"].ToString();
                txtClientprice.Text = dt.Rows[0]["ClientPrice"].ToString();
                txtAuthorityFee.Text = dt.Rows[0]["AuthorityFee"].ToString();
                txtConsultantDescription.Text = dt.Rows[0]["ConsultantDescription"].ToString();
                ddlConsultantETC.SelectedIndex = ddlConsultantETC.Items.IndexOf(ddlConsultantETC.Items.FindByValue(dt.Rows[0]["ConsultantETC"].ToString()));
                txtClientDescription.Text = dt.Rows[0]["ClientDescription"].ToString();
                ddlClientETC.SelectedIndex = ddlClientETC.Items.IndexOf(ddlClientETC.Items.FindByValue(dt.Rows[0]["ClientETC"].ToString()));
                chkactive.Checked = bool.Parse(dt.Rows[0]["isActive"].ToString());
                SetList(ddlAssignmentPeriod, PL.dt.Rows[0]["PeriodId"].ToString());
                FillQuesListView(dt.Rows[0]["Questionnaire"].ToString());
                SavegetQuestions(Convert.ToInt32(dt.Rows[0]["Autoid"].ToString()), groupId);
                ViewState["Mode"] = "Edit";
                divView.Visible = false;
                divAddEdit.Visible = true;
            }
            else
            {
                ClearField();
            }
        }
        void SavegetQuestions(int mainid, int groupid)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 11;
            PL.AutoId = mainid;
            PL.GroupId = groupid;
            ServiceMasterDL.returnTable(PL);
            DataTable dt2 = PL.dt;
            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow row in dt2.Rows)
                {
                    foreach (ListViewItem item2 in LVQuesName.Items)
                    {
                        CheckBox chkSelect2 = (CheckBox)item2.FindControl("chkDocumentQu");
                        if (chkSelect2.Attributes["Documentid"].ToString() == row["DocumentId"].ToString())
                        {
                            CheckBox IsRequired = (CheckBox)item2.FindControl("chkIsRequiredQu");
                            CheckBox IsRequiredOp = (CheckBox)item2.FindControl("chkIsRequiredQuOp");
                            if (row["IsRequired"].ToString() == "True")
                            {
                                IsRequired.Checked = true;
                            }
                            if (row["IsRequired"].ToString() == "False")
                            {
                                IsRequired.Checked = false;
                            }
                            if (row["IsRequiredOperation"].ToString() == "True")
                            {
                                IsRequiredOp.Checked = true;
                            }
                            if (row["IsRequiredOperation"].ToString() == "False")
                            {
                                IsRequiredOp.Checked = false;
                            }
                            chkSelect2.Checked = true;
                            break;
                        }
                    }

                }
            }
        }
        void SetList(ListBox ddl, string ids)
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
        string GetParentServiceXml()
        {
            string xml = "<tbl>";
            xml += "<tr>";
            xml += "<ServiceName><![CDATA[" + txtServiceName.Text.Trim() + "]]></ServiceName>";
            xml += "<ServiceTypeAutoid><![CDATA[" + ddlServiceType.SelectedValue + "]]></ServiceTypeAutoid>";
            xml += "</tr>";
            xml += "</tbl>";
            return xml;
        }
        string XMLField(string mainid, string GroupId)
        {
            string xml = "<tr>";
            xml += "<ServiceMainId><![CDATA[" + mainid + "]]></ServiceMainId>";
            xml += "<Questionnaire><![CDATA[" + ddlQuestionnaire.SelectedValue + "]]></Questionnaire>";
            xml += "<ConsultantPrice><![CDATA[" + txtConsultantPrice.Text.Trim() + "]]></ConsultantPrice>";
            xml += "<ClientPrice><![CDATA[" + txtClientprice.Text.Trim() + "]]></ClientPrice>";
            xml += "<AuthorityFee><![CDATA[" + txtAuthorityFee.Text.Trim() + "]]></AuthorityFee>";
            xml += "<ConsultantDescription><![CDATA[" + txtConsultantDescription.Text.Trim() + "]]></ConsultantDescription>";
            xml += "<ConsultantETC><![CDATA[" + ddlConsultantETC.SelectedValue + "]]></ConsultantETC>";
            xml += "<ClientDescription><![CDATA[" + txtClientDescription.Text.Trim() + "]]></ClientDescription>";
            xml += "<ClientETC><![CDATA[" + ddlClientETC.SelectedValue + "]]></ClientETC>";
            xml += "<MainRemarks><![CDATA[" + txtMainRemarks.Text.Trim() + "]]></MainRemarks>";
            xml += "<GroupId><![CDATA[" + GroupId + "]]></GroupId>";
            xml += "<IsActive><![CDATA[" + (chkactive.Checked) + "]]></IsActive>";
            xml += "</tr>";
            return xml;
        }
        void GetChildServiceXml(string ServiceMainId)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            foreach (ListItem gr in chkGroupCompany.Items)
            {
                if (gr.Selected)
                {
                    string xml = "<tbl>";
                    xml += XMLField(ServiceMainId, gr.Value);
                    xml += "</tbl>";
                    ///////////////////////
                    PL.OpCode = 9;
                    PL.XML1 = xml;
                    ServiceMasterDL.returnTable(PL);
                    DataTable dtq = PL.dt;
                    if (!PL.isException)
                    {
                        string GroupServiceMainId = dtq.Rows[0]["GroupServiceMainId"].ToString();
                        string XML2 = "<tbl>";
                        foreach (ListViewItem item in LVQuesName.Items)
                        {
                            CheckBox chkSelect = (CheckBox)item.FindControl("chkDocumentQu");
                            if (chkSelect != null)
                            {
                                if (chkSelect.Checked)
                                {
                                    CheckBox chkIsrequired = (CheckBox)item.FindControl("chkIsRequiredQu");
                                    CheckBox chkIsrequiredOpr = (CheckBox)item.FindControl("chkIsRequiredQuOp");
                                    int Documentid = Convert.ToInt32(chkSelect.Attributes["Documentid"]);
                                    XML2 += XMLDataQuestion(Documentid, chkIsrequired, chkIsrequiredOpr, GroupServiceMainId);
                                }
                            }
                        }
                        XML2 += "</tbl>";
                        PL.OpCode = 10;
                        PL.XML2 = XML2;
                        ServiceMasterDL.returnTable(PL);
                    }
                }
            }
        }
        string saveGroup()
        {
            string XML = "";
            string groupString = Request.Form[ddlAssignmentPeriod.UniqueID];
            if (groupString != null)
            {
                var query = from val in groupString.Split(',')
                            select int.Parse(val);
                XML += "<tbl>";
                foreach (int num in query)
                {
                    XML += XMLGroup(num);
                }
                XML += "</tbl>";
            }
            return XML;
        }
        private static string XMLGroup(int GroupId)
        {
            string XML = "<tr>";
            XML += "<PeriodId><![CDATA[" + GroupId + "]]></PeriodId>";
            XML += "</tr>";
            return XML;
        }
        private static string XMLDataQuestion(int Documentid, CheckBox chkIsrequired, CheckBox chkIsrequiredOpr,string GroupServiceMainId)
        {
            string XML = "<tr>";
            XML += "<Autoid><![CDATA[" + GroupServiceMainId + "]]></Autoid>";
            XML += "<Documentid><![CDATA[" + Documentid + "]]></Documentid>";
            XML += "<Isrequired><![CDATA[" + (chkIsrequired.Checked == true ? 1 : 0) + "]]></Isrequired>";
            XML += "<IsRequiredOperation><![CDATA[" + (chkIsrequiredOpr.Checked == true ? 1 : 0) + "]]></IsRequiredOperation>";
            XML += "</tr>";
            return XML;
        }
        string GetChildServiceTypeXmlForUpdate()
        {
            string xml = "<tbl>";
            //xml += "<tr>";
            //xml += "<Manager><![CDATA[" + ddlManager.SelectedValue + "]]></Manager>";
            //xml += "<AsstManager><![CDATA[" + ddlAsstManager.SelectedValue + "]]></AsstManager>";
            //xml += "<Supervisor><![CDATA[" + ddlPrimarySupervisor.SelectedValue + "]]></Supervisor>";
            //xml += "<SecondarySupervisor><![CDATA[" + ddlSecondarySupervisor.SelectedValue + "]]></SecondarySupervisor>";
            //xml += "<PrimaryAccessTo><![CDATA[" + ddlPrimaryEA.SelectedValue + "]]></PrimaryAccessTo>";
            //xml += "<SecondaryAccessTo><![CDATA[" + ddlSecondaryEA.SelectedValue + "]]></SecondaryAccessTo>";
            //xml += "<Assigner><![CDATA[" + ddlPrimaryAssigner.SelectedValue + "]]></Assigner>";
            //xml += "<SecondaryAssigner><![CDATA[" + ddlSecondaryAssigner.SelectedValue + "]]></SecondaryAssigner>";
            //xml += "<PrimaryReviewer><![CDATA[" + ddlPrimaryReviewer.SelectedValue + "]]></PrimaryReviewer>";
            //xml += "<SecondaryReviewer><![CDATA[" + ddlSecondaryReviewer.SelectedValue + "]]></SecondaryReviewer>";
            //xml += "<GroupId><![CDATA[" + ddlUpdateGroupCompany.SelectedValue + "]]></GroupId>";
            //xml += "<IsActive><![CDATA[" + (chkactive.Checked) + "]]></IsActive>";
            //xml += "</tr>";
            //xml += "</tbl>";
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
        void GetChildServiceXmlForUpdate(string ServiceMainId, string GroupId)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            string xml = "<tbl>";
            xml += XMLField(ServiceMainId, GroupId);
            xml += "</tbl>";
            ///////////////////////
            ServiceMasterDL.returnTable(PL);
            if (!PL.isException)
            {
                string XML2 = "<tbl>";
                foreach (ListViewItem item in LVQuesName.Items)
                {
                    CheckBox chkSelect = (CheckBox)item.FindControl("chkDocumentQu");
                    if (chkSelect != null)
                    {
                        if (chkSelect.Checked)
                        {
                            CheckBox chkIsrequired = (CheckBox)item.FindControl("chkIsRequiredQu");
                            CheckBox chkIsrequiredOpr = (CheckBox)item.FindControl("chkIsRequiredQuOp");
                            int Documentid = Convert.ToInt32(chkSelect.Attributes["Documentid"]);
                            XML2 += XMLDataQuestion(Documentid, chkIsrequired, chkIsrequiredOpr, ServiceMainId);
                        }
                    }
                }
                XML2 += "</tbl>";
                PL.OpCode = 13;
                PL.AutoId = ServiceMainId;
                PL.GroupId = GroupId;
                PL.XML1 = xml;
                PL.XML2 = XML2;
                ServiceMasterDL.returnTable(PL);
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (IsAnyItemChecked() || ddlUpdateGroupCompany.SelectedValue != "")
            {
                ServiceMasterPL PL = new ServiceMasterPL();
                if (ViewState["Mode"].ToString() == "Add")
                {
                    PL.OpCode = 8;
                    PL.XML = GetParentServiceXml();
                    PL.XML1 = saveGroup();
                    PL.CreatedBy = Session["UserAutoId"].ToString();
                    ServiceMasterDL.returnTable(PL);
                    if (!PL.isException)
                    {
                        GetChildServiceXml(PL.dt.Rows[0]["ServiceMainId"].ToString());
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
                    PL.OpCode = 12;
                    PL.XML = GetParentServiceXml();
                    PL.XML1 = saveGroup();
                    PL.AutoId = Convert.ToInt32(hidID.Value);
                    PL.CreatedBy = Session["UserAutoId"].ToString();
                    ServiceMasterDL.returnTable(PL);
                    if (!PL.isException)
                    {
                        GetChildServiceXmlForUpdate(hidID.Value, ddlUpdateGroupCompany.SelectedValue);
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
                FillGroup();
                FillListView();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "flag", "ShowError('Select atleast one Company');", true);
            }
        }
        void FillQuesListView(string questionnaire)
        {
            //-----------------
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 6;
            PL.OldName = questionnaire;
            ServiceMasterDL.returnTable(PL);
            DataTable dt = PL.dt;
            //--------------------------------
            LVQuesName.DataSource = dt;
            LVQuesName.DataBind();
        }
        void addAssignmentDefault(string questionnaire)
        {
            FillQuesListView(questionnaire);
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 7;
            ServiceMasterDL.returnTable(PL);
            DataTable dt = PL.dt;
            if (PL.dt.Rows.Count > 0)
            {
                foreach (DataRow row in PL.dt.Rows)
                {
                    foreach (ListViewItem item2 in LVQuesName.Items)
                    {
                        CheckBox chkSelect2 = (CheckBox)item2.FindControl("chkDocumentQu");
                        if (chkSelect2.Attributes["Documentid"].ToString() == row["Autoid"].ToString())
                        {
                            CheckBox IsRequired = (CheckBox)item2.FindControl("chkDocumentQu");
                            if (row["isMandatory"].ToString() == "True")
                            {
                                IsRequired.Checked = true;
                            }
                            if (row["isMandatory"].ToString() == "False")
                            {
                                IsRequired.Checked = false;
                            }
                            break;
                        }
                    }
                }
            }
        }
        protected void ddlQuestionnaire_SelectedIndexChanged(object sender, EventArgs e)
        {
            addAssignmentDefault(ddlQuestionnaire.SelectedValue);
        }
        [System.Web.Services.WebMethod]
        public static string CheckName(string text, string oldname)
        {
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 14;
            PL.Type = text;
            PL.OldName = oldname;
            ServiceMasterDL.returnTable(PL); ;
            return PL.dt.Rows[0]["count"].ToString();
        }
        [System.Web.Services.WebMethod]
        public static string QuestionsData(string DocumentId)
        {
            string jsondata = "";
            ServiceMasterPL PL = new ServiceMasterPL();
            PL.OpCode = 15;
            PL.AutoId = Convert.ToInt32(DocumentId);
            ServiceMasterDL.returnTable(PL);
            if (PL.dt.Rows.Count > 0)
            {
                jsondata = new clsGeneral().JSONfromDT(PL.dt);
            }
            return jsondata;
        }
    }
}